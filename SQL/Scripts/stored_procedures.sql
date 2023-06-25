use `battlespire`;


--    ___              _           _                      _   
--   / __|_ _ ___ __ _| |_ ___    /_\  __ __ ___ _  _ _ _| |_ 
--  | (__| '_/ -_) _` |  _/ -_)  / _ \/ _/ _/ _ \ || | ' \  _|
--   \___|_| \___\__,_|\__\___| /_/ \_\__\__\___/\_,_|_||_\__|
--                                                            
drop procedure if exists CreateAccount;
delimiter //
create procedure CreateAccount(in _username varchar(50), in _email varchar(100), in _password varchar(50))
begin

	declare _account_id int;


	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if (_username is null or _username = '') or  (_email is null or _email = '')  or (_password is null or _password = '') 
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	set autocommit = off;
	start transaction;
	
		-- insert into accountt (username, email, password) values ('a', 'a', 'a');
	
		case
			when _email in (
				select email
				from account
			)
			then 
				signal sqlstate '45000'
					set message_text = 'Error: The Email already exists.', mysql_errno = 1000;
			when _username in (
				select username
				from account
			)
			then
				signal sqlstate '45000'
					set message_text = 'Error: The Username already exists.', mysql_errno = 1000;
			else insert into account (username, email, password)
		    values (_username, _email, _password);
		end case;
	commit;
	
	select account_id 
		into _account_id
	from account 
		where username = _username;
	
	call createplayer(_account_id);

	select 'Success: User Created' as message;
end //
delimiter ;     




--   _              _          _                      _   
--  | |   ___  __ _(_)_ _     /_\  __ __ ___ _  _ _ _| |_ 
--  | |__/ _ \/ _` | | ' \   / _ \/ _/ _/ _ \ || | ' \  _|
--  |____\___/\__, |_|_||_| /_/ \_\__\__\___/\_,_|_||_\__|
--            |___/                                       
-- 
drop procedure if exists LoginAccount;
delimiter //
create procedure LoginAccount(in _username varchar(50), in _password varchar(50))
login:begin
	
	declare _accountId int;
	declare _playerId int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if (_username is null or _username = '')  or (_password is null or _password = '') 
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;



	select e.entity_id 
		into _playerId 
	from entity e 
	join account a 
		on a.account_id = e.account_id 
		where a.username = _username;
	
	
	select account_id 
		into _accountId 
	from account 
		where username = _username;
	
	set autocommit = off;
	start transaction;
		if (
			select attempts
			from account
			where username = _username
		) >= 5
		then
			signal sqlstate '45000'
				set message_text = 'Error: This account is locked, please contact the administrator.', mysql_errno = 1000;
			leave login;
		end if;
		
		if (
			select username
			from account
			where username = _username
			and password = _password
			and is_logged_in = false 
		) = _username
		then
			select 'Success: Login succesful.' as message;
			if _playerId is null then 
				call createplayer(_accountId);
			end if;
		else
			update account 
			set attempts  = attempts + 1 
			where username = _username;
			commit;
			signal sqlstate '45000'
				set message_text = 'Error: The Login attempt failed.', mysql_errno = 1000;
		end if;
	commit;

end //
delimiter ;  


    

--    ___                       _         __  __           
--   / __|___ _ _  ___ _ _ __ _| |_ ___  |  \/  |__ _ _ __ 
--  | (_ / -_) ' \/ -_) '_/ _` |  _/ -_) | |\/| / _` | '_ \
--   \___\___|_||_\___|_| \__,_|\__\___| |_|  |_\__,_| .__/
--                                                   |_|   
-- 
drop procedure if exists GenerateMap;
delimiter //
create procedure GenerateMap( in _width int, in _height int)
begin
	
	declare _x int default _width * -1;
	declare _y int default _height * -1;
	declare _type varchar(50) default "ground";
	declare _type_mod int;
	declare _total_entities int default _width + _height;
	declare _index int default 0;
	declare _tile int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if (_width is null or _width = 0)  or (_height is null or _height = 0) 
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	set autocommit = off;
	start transaction;
		while _x <= _width do
			while _y <= _height do
				
				
				set _type_mod = ceil(rand() * 100);
			
				
				if _type_mod <= 1 && _type_mod >= 0 then
					set _type = "exit";
				elseif _type_mod <= 25 && _type_mod > 1 then
					set _type = "wall";
				else 
					set _type = "ground";
				end if;
				if _x = 0 and _y = 0 then 
					set _type = "ground";
				end if;
			
		
				
			
				insert into tile (x, y, tile_type)
		        values (_x, _y, _type);
				set _y = _y + 1;
			end while;
		    set _y = _height * -1;
		    set _x = _x + 1;
		end while;
	commit;

	while _index < _total_entities do
	
	
		select tile_id
			into _tile
		from tile t 
			where tile_type = "ground" 
				&& ((_x >= 10 or _x <= -10)
				&& (_y >= 10 or _y <= -10))
				&& tile_id not in (
					select t2.tile_id
					from tile t2
						join entity e 
						on e.tile_id = t2.tile_id
						where t2.tile_type = "ground")
			order by rand()
			limit 1; 
	
		
			if ceil(rand() * 10) < 5 then
				call SpawnMonster(_tile);
			else
				call SpawnChest(_tile);
			end if;
				
		set _index = _index +1;
	end while;
end //
delimiter ;




--    ___                       _         ___                 _                
--   / __|___ _ _  ___ _ _ __ _| |_ ___  |_ _|_ ___ _____ _ _| |_ ___ _ _ _  _ 
--  | (_ / -_) ' \/ -_) '_/ _` |  _/ -_)  | || ' \ V / -_) ' \  _/ _ \ '_| || |
--   \___\___|_||_\___|_| \__,_|\__\___| |___|_||_\_/\___|_||_\__\___/_|  \_, |
--                                                                        |__/ 
-- 
drop procedure if exists GenerateInventory;
delimiter //
create procedure GenerateInventory(in _entity_id int, in _width int, in _height int)
begin
	declare _x int default 0;
	declare _y int default 0;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _entity_id is null  or (_width is null or _width = 0)  or (_height is null or _height = 0) 
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	set autocommit = off;
	start transaction;
		while _x <= _width do
			while _y <= _height do
					insert into tile (x, y, tile_type, owner_id)
			        values (_x, _y, "inventory", _entity_id);
					set _y = _y + 1;
			end while;
		    set _y = _height * 0;
		    set _x = _x + 1;
		end while;
	commit;
end //
delimiter ;







--    ___              _         ___ _                   
--   / __|_ _ ___ __ _| |_ ___  | _ \ |__ _ _  _ ___ _ _ 
--  | (__| '_/ -_) _` |  _/ -_) |  _/ / _` | || / -_) '_|
--   \___|_| \___\__,_|\__\___| |_| |_\__,_|\_, \___|_|  
--                                          |__/         
-- 
drop procedure if exists CreatePlayer;
delimiter //
create procedure CreatePlayer(in _account_id int)
begin
	declare _home_tile int;
	declare _player_id int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _account_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	select tile_id 
	into _home_tile
	from tile
	where x = 0 and y = 0 and tile_type = "ground";
	
	set autocommit = off;
	start transaction;
		insert into entity (
			account_id,
			entity_type,
			health,
			damage_taken,
			attack,
			defense,
			healing,
			tile_id,
			killscore
		)
		values (
			_account_id,
			"player",
			10,
			0,
			10,
			10,
			10,
			_home_tile,
			0
		);

		select entity_id
		into _player_id
		from entity
		where account_id = _account_id;
	
		call GenerateInventory(_player_id, 8, 4);

	commit;
end //
delimiter ;






--   __  __               ___                 _                  ___ _             
--  |  \/  |_____ _____  |_ _|_ ___ _____ _ _| |_ ___ _ _ _  _  |_ _| |_ ___ _ __  
--  | |\/| / _ \ V / -_)  | || ' \ V / -_) ' \  _/ _ \ '_| || |  | ||  _/ -_) '  \ 
--  |_|  |_\___/\_/\___| |___|_||_\_/\___|_||_\__\___/_|  \_, | |___|\__\___|_|_|_|
--                                                        |__/                     
-- 
-- procedure should take an account_id and an item_id, it should change the owner_id,
-- then update the inventory_used of the player and the chest the item came from.
-- should allow players to equip item from chest directly.
drop procedure if exists MoveInventoryItem;
delimiter //
create procedure MoveInventoryItem(in _item_id int, in _origin_tile_id int, in _target_tile_id int)
begin
	
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _target_tile_id is null or _origin_tile_id is null or _item_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	set autocommit = off;
	start transaction;


		update entity 
		set tile_id = _target_tile_id
		where tile_id = _origin_tile_id
		and entity_id = _item_id;

	
	commit;

end //
delimiter ;




--    ___              _         ___ _             
--   / __|_ _ ___ __ _| |_ ___  |_ _| |_ ___ _ __  
--  | (__| '_/ -_) _` |  _/ -_)  | ||  _/ -_) '  \ 
--   \___|_| \___\__,_|\__\___| |___|\__\___|_|_|_|
--                               
drop procedure if exists CreateItem;
delimiter //
create procedure CreateItem(in _chest_id int)
begin
	declare _health int default 0;
	declare _attack int default 0;
	declare _defense int default 0;
	declare _healing int default 0;
	
	declare _type_mod int;
	declare _item_type varchar(50) default "Sword";
	declare _tier varchar(10) default "I";
	
	declare _x int;
	declare _y int;
	declare _inventory_tile int;
	

	-- find the distance of the chest from the center
	declare _distance int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _chest_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	set autocommit = off;
	start transaction;

		select ceil(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)))
		into _distance
		from entity e 
		join tile t
			on t.tile_id = e.tile_id
		where
			e.entity_id = _chest_id;
		
		if _distance < 1 then
			set _distance = 1;
		end if;
	
	
		set _type_mod = ceil(rand() *4);
	
		if _type_mod = 1 then
			set _item_type = "Armour ";
			set _health = pow(_distance, 1.25);
		end if;
		if _type_mod = 2 then
			set _item_type = "Sword ";
			set _attack = pow(_distance, 1.25) / 5;
		end if;
		if _type_mod = 3 then
			set _item_type = "Shield ";
			set _defense = pow(_distance, 1.25) / 5;
		end if;
		if _type_mod = 4 then
			set _item_type = "Amulet ";
			set _healing = pow(_distance, 1.25) / 10;
		end if;
		
		
		-- determine the tier
	    set _tier = CalculateTier(_distance);

		select t.tile_id 
			into _inventory_tile
		from tile t 
			where t.owner_id = _chest_id 
			and t.tile_id not in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = _chest_id
				)
			order by rand()
			limit 1;
		

		insert into entity (name, health, attack, defense, healing, entity_type, owner_id, tile_id, is_equipped)
		values (
			concat(_item_type, _tier),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _health),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _attack),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _defense),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _healing),
			"item",
			_chest_id,
			_inventory_tile,
			0
		);
	commit;
end //
delimiter ;




--   ___                           ___ _           _   
--  / __|_ __  __ ___ __ ___ _    / __| |_  ___ __| |_ 
--  \__ \ '_ \/ _` \ V  V / ' \  | (__| ' \/ -_|_-<  _|
--  |___/ .__/\__,_|\_/\_/|_||_|  \___|_||_\___/__/\__|
--      |_|                                            
-- 
drop procedure if exists SpawnChest;
delimiter //
create procedure SpawnChest(in _tile_id int)
begin
	
	declare _item_total int;
	declare _items_created int default 0;
	declare _chest_id int;
	
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _tile_id is null 
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	insert into entity (entity_type, tile_id)
	values ("chest", _tile_id);

	set _chest_id =  GetChestIDFromTile(_tile_id);

	call GenerateInventory(_chest_id, 8, 4);

	set _item_total = ceil(rand() *2);

	while _items_created < _item_total do
		call CreateItem(_chest_id);
		set _items_created = _items_created + 1;
	end while;

end //
delimiter ;





--   ___                          __  __             _           
--  / __|_ __  __ ___ __ ___ _   |  \/  |___ _ _  __| |_ ___ _ _ 
--  \__ \ '_ \/ _` \ V  V / ' \  | |\/| / _ \ ' \(_-<  _/ -_) '_|
--  |___/ .__/\__,_|\_/\_/|_||_| |_|  |_\___/_||_/__/\__\___|_|  
--      |_|                                                      
-- 
drop procedure if exists SpawnMonster;
delimiter //
create procedure SpawnMonster(in _tile_id int)
begin

	declare _health int;
	declare _attack int;
	declare _defense int;
	declare _healing int;
	
	declare _type_mod int ;
	declare _monster_type varchar(50) default "Goblin ";
	declare _tier varchar(10) default "I";
	
	-- find the distance of the chest from the center
	declare _distance int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _tile_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	
	select ceil(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)))
	into _distance
	from tile t 
	where tile_id = _tile_id;


	set _type_mod = ceil(rand() *4);
	set _health = pow(_distance, 1.25);
	set _attack = pow(_distance, 1.25) / 5;
	set _defense = pow(_distance, 1.25) / 5;
	set _healing = pow(_distance, 1.25) / 10;
	


	if _type_mod = 1 then
		set _monster_type = "Goblin ";
	end if;

	if _type_mod = 2 then
		set _monster_type = "Skeleton ";
	end if;

	if _type_mod = 3 then
		set _monster_type = "Draugr ";
	end if;

	if _type_mod = 4 then
		set _monster_type = "Scamp ";
	end if;

	-- determine the tier
    set _tier = CalculateTier(_distance);
	

	set autocommit = off;
	start transaction;
		-- create the new monster
		insert into entity (name, health, damage_taken, attack, defense, healing, entity_type, tile_id)
		values (
			concat(_monster_type, _tier),
			_health,
			0,
			ceil((rand() * (1.3 - 0.7) + 0.7) * _attack),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _defense),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _healing),
			"monster",
			_tile_id
		);
	commit;
end //
delimiter ;



--   ___        _      _       _          _                      _   
--  |_ _|___   /_\  __| |_ __ (_)_ _     /_\  __ __ ___ _  _ _ _| |_ 
--   | |(_-<  / _ \/ _` | '  \| | ' \   / _ \/ _/ _/ _ \ || | ' \  _|
--  |___/__/ /_/ \_\__,_|_|_|_|_|_||_| /_/ \_\__\__\___/\_,_|_||_\__|
--                                                                   
drop procedure if exists IsAdminAccount;
delimiter //
create procedure IsAdminAccount(in _username varchar(50))
begin
	
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _username is null or _username = ''
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;
	
	select is_administrator 
	from account
	where username =_username;
	
end //
delimiter ;



--    ___     _       _   _ _   ___ _                      
--   / __|___| |_    /_\ | | | | _ \ |__ _ _  _ ___ _ _ ___
--  | (_ / -_)  _|  / _ \| | | |  _/ / _` | || / -_) '_(_-<
--   \___\___|\__| /_/ \_\_|_| |_| |_\__,_|\_, \___|_| /__/
--                                         |__/            
-- 
drop procedure if exists GetAllPlayers;
delimiter //
create procedure GetAllPlayers()
begin
	
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;
	
	select username
	from account a
	join entity e 
	on a.account_id = e.account_id;
	
end //
delimiter ;




--    ___     _     _                _         _                      _ 
--   / __|___| |_  | |   ___ __ _ __| |___ _ _| |__  ___  __ _ _ _ __| |
--  | (_ / -_)  _| | |__/ -_) _` / _` / -_) '_| '_ \/ _ \/ _` | '_/ _` |
--   \___\___|\__| |____\___\__,_\__,_\___|_| |_.__/\___/\__,_|_| \__,_|
--                                                                      
drop procedure if exists GetLeaderboard;
delimiter //
create procedure GetLeaderboard()
begin
	
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

	
	select concat(username, ': ', highscore)
	from account
	order by highscore desc;
	
end //
delimiter ;



--    ___     _      ___ _         _     _  _ _    _                
--   / __|___| |_   / __| |_  __ _| |_  | || (_)__| |_ ___ _ _ _  _ 
--  | (_ / -_)  _| | (__| ' \/ _` |  _| | __ | (_-<  _/ _ \ '_| || |
--   \___\___|\__|  \___|_||_\__,_|\__| |_||_|_/__/\__\___/_|  \_, |
--                                                             |__/ 
-- 
drop procedure if exists GetChatHistory;
delimiter //
create procedure GetChatHistory()
begin
	
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

	
	select concat('<',time(m.sent_time),'> ',a.username,': ', message) as message
	from message m
	join account a
	on m.account_id = a.account_id
    order by sent_time asc ;
	
end //
delimiter ;


--   ___              _   __  __                          
--  / __| ___ _ _  __| | |  \/  |___ ______ __ _ __ _ ___ 
--  \__ \/ -_) ' \/ _` | | |\/| / -_|_-<_-</ _` / _` / -_)
--  |___/\___|_||_\__,_| |_|  |_\___/__/__/\__,_\__, \___|
--                                              |___/     
-- 
drop procedure if exists SendMessage;
delimiter //
create procedure SendMessage(in _account_id int, in _message varchar(500))
begin
	
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _account_id is null or _message is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;
	
	set autocommit = off;
	start transaction;
		insert into message (account_id, message, sent_time)
		values (_account_id, _message, current_timestamp());	
	commit;

end //
delimiter ;



--    ___     _     _____ _ _          ___        ___ _                   
--   / __|___| |_  |_   _(_) |___ ___ | _ )_  _  | _ \ |__ _ _  _ ___ _ _ 
--  | (_ / -_)  _|   | | | | / -_|_-< | _ \ || | |  _/ / _` | || / -_) '_|
--   \___\___|\__|   |_| |_|_\___/__/ |___/\_, | |_| |_\__,_|\_, \___|_|  
--                                         |__/              |__/         
-- 
drop procedure if exists GetTilesByPlayer;
delimiter //
create procedure GetTilesByPlayer(in _player_id int, in _viewport_width int, in _viewport_height int)
begin

	
	declare _x int;
	declare _y int;
	declare _width int;
	declare _height int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _player_id is null or (_viewport_width is null or _viewport_width = 0)  or (_viewport_height is null or _viewport_height = 0) 
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	set _width = _viewport_width /2;
	set _height = _viewport_height /2;
	
	select x,y
	into _x,_y	
	from tile t 
	join entity e on e.tile_id = t.tile_id 
	where entity_id = _player_id;

	select tile_id, x, y, tile_type
	from tile
	where 
		tile_type = 'ground'  &&
		x >= _x - _width &&
		x <= _x + _width &&
		y >= _y - _height &&
		y <= _y + _height ||
		tile_type = 'wall' &&
		x >= _x - _width &&
		x <= _x + _width &&
		y >= _y - _height &&
		y <= _y + _height  ||
		tile_type = 'exit' &&
		x >= _x - _width &&
		x <= _x + _width &&
		y >= _y - _height &&
		y <= _y + _height 
	order by x desc, y asc;

end //
delimiter ;

drop procedure if exists GetPlayerByAccUsername;
delimiter //
create procedure GetPlayerByAccUsername(in _username varchar(50))
begin
	
	declare _accountId int;
	declare _playerId int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _username is null 
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	

	select  e.entity_id 
		into _playerId
	from account a 
		join entity e
		on e.account_id = a.account_id 
		where a.username = _username;

	if isnull(_playerId) then
	
		select  a.account_id 
			into _accountId
		from account a 
			where a.username = _username;
		
		call createplayer(_accountId);
		
	end if;
	
	
		select *
		from entity e 
			join account a on a.account_id = e.account_id 
			where a.username = _username;


	
end //
delimiter ; 

   
drop procedure if exists GetAllEntities; -- excluding items
delimiter //
create procedure GetAllEntities(in _player_id int)
begin
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _player_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	
	
	select *
	from entity e 
	where e.entity_type != "item"
	and e.entity_id != _player_id;

	
end //
delimiter ;  

 

drop procedure if exists MovePlayer; 
delimiter //
create procedure MovePlayer(in _target_tile int, in _player_id int)
begin
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _target_tile is null or _player_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	
	
	set autocommit = off;
	start transaction;
		update entity 
		set tile_id = _target_tile
		where entity_id = _player_id;
	commit;
	
end //
delimiter ; 


drop procedure if exists MoveMonsterNPC;
delimiter //
create procedure MoveMonsterNPC(in p_monster_id int)
begin
	
	declare _x int;
	declare _y int;
	declare _target_tile int;
	declare _count int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if p_monster_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	

  	-- get the tile id of the monster
	select x, y
	into _x, _y
	from tile t 
	join entity e on e.tile_id = t.tile_id 
	where e.entity_id = p_monster_id;

	--
	select tile_id
	into _target_tile
	from tile t 
	where 
		tile_type = 'ground'  &&
		x >= _x - 1 &&
		x <= _x + 1 &&
		y >= _y - 1 &&
		y <= _y + 1
	order by rand()
	limit 1;

	select _target_tile;
	
	
	
	set autocommit = off;
	start transaction;

		update entity 
		set tile_id = _target_tile
		where entity_id = p_monster_id;
	
	commit;
	
end //
delimiter ; 



drop procedure if exists GetEntityInventory; 
delimiter //
create procedure GetEntityInventory(in _entity_id int)
begin
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _entity_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
		
	select * 
	from entity 
	where entity_type = "item"
	and owner_id = _entity_id;

	
end //
delimiter ; 

drop procedure if exists GetEntityInventoryTiles; 
delimiter //
create procedure GetEntityInventoryTiles(in _entity_id int)
begin
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _entity_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	
	
		
	select * 
	from tile 
	where tile_type = 'inventory'
	and owner_id = _entity_id;

	
end //
delimiter ; 


drop procedure if exists EquipItem;
delimiter //
create procedure EquipItem(in _player_id int, in _item_id int)
begin
	
	
	declare new_item_name varchar(50);
	declare already_equipped int;
	declare already_equipped_id int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _player_id is null or  _item_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	
	select name, is_equipped 
	into new_item_name, already_equipped
	from entity e 
	where e.entity_id = _item_id;

	set autocommit = off;
	start transaction;
	if already_equipped = 1 then

		update entity 
		set is_equipped = 0
		where entity_id = _item_id;
	else
		

		if new_item_name like 'Sword%' then
			select entity_id, is_equipped
			into already_equipped_id, already_equipped
			from entity e 
			where owner_id = _player_id
			and is_equipped = 1
			and name like 'Sword%';
			
			
		elseif new_item_name like 'Armour%' then
			select entity_id, is_equipped
			into already_equipped_id, already_equipped
			from entity e 
			where owner_id = _player_id
			and is_equipped = 1
			and name like 'Armour%';
			
		elseif new_item_name like 'Amulet%' then
			select entity_id, is_equipped
			into already_equipped_id, already_equipped
			from entity e 
			where owner_id = _player_id
			and is_equipped = 1
			and name like 'Amulet%';
			
		else
			select entity_id, is_equipped
			into already_equipped_id, already_equipped
			from entity e 
			where owner_id = _player_id
			and is_equipped = 1
			and name like 'Shield%';
			
		end if;
	
		if already_equipped = 1 then
			
			update entity 
			set is_equipped = 0
			where entity_id = already_equipped_id;
		end if;
	
		update entity 
		set is_equipped = 1
		where entity_id = _item_id;
	end if;

		commit;

end //
delimiter ; 




drop procedure if exists TransferItem;
delimiter //
create procedure TransferItem(in _item_id int, in _player_id int)
begin
	
	
	declare new_tile int;


	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _item_id is null or _player_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	
	
	select t.tile_id 
		into new_tile
	from tile t 
		where t.owner_id = _player_id 
		and t.tile_id not in (
			select t2.tile_id
			from tile t2
				join entity e 
					on t2.owner_id = e.entity_id 
				join entity e2 
					on e2.tile_id = t2.tile_id 
				where e.entity_id = _player_id
			)
		order by rand()
		limit 1;

	set autocommit = off;
	start transaction;

		update entity 
		set tile_id = new_tile, owner_id = _player_id
		where entity_id = _item_id;

	commit;

end //
delimiter ; 



drop procedure if exists DamageEntity;
delimiter //
create procedure DamageEntity(in _attacker_id int, in _defender_id int)
begin
	
	declare defender_health int;
	declare defender_defense int;
	declare defender_attack int;
	declare defender_damage_taken int;

	declare attacker_health int;
	declare attacker_defense int;
	declare attacker_attack int;
	declare attacker_damage_taken int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _attacker_id is null or _defender_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	


	select sum(health), sum(attack), sum(defense)
		into defender_health, defender_attack, defender_defense
	from entity e 
		where e.entity_id = _defender_id
		or (is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = _defender_id
				));
			
	
	select sum(health), sum(attack), sum(defense)
		into attacker_health, attacker_attack, attacker_defense
	from entity e 
		where e.entity_id =	_attacker_id
		or (is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = _attacker_id
				));
			
			
	
	
	
	set autocommit = off;
	start transaction;

		if (attacker_attack > defender_defense / 2) then
		
			update entity 
			set damage_taken = damage_taken + attacker_attack - (defender_defense /2)
			where entity_id = _defender_id;
			
		end if;
	
		if (defender_attack > attacker_defense / 2) then
		
			update entity 
			set damage_taken = damage_taken + defender_attack - (attacker_defense /2)
			where entity_id = _attacker_id;
			
		end if;
		
		-- call CheckEnti
	commit;
	call CheckEntityStatus(_attacker_id);
	call CheckEntityStatus(_defender_id);
end //
delimiter ; 



drop procedure if exists GetPlayerStats;
delimiter //
create procedure GetPlayerStats(in _player_id int)
begin
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _player_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	
	

	select sum(health), sum(attack), sum(defense), sum(healing), sum(damage_taken)
	from entity e 
		where entity_id = _player_id or
		(is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = _player_id
				));

	call checkentitystatus(_player_id);
end //
delimiter ; 



drop procedure if exists CheckEntityStatus;
delimiter //
create procedure CheckEntityStatus(in _entity_id int)
begin

	
	
	declare entity_health int;
	declare entity_damage int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _entity_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	select sum(health), sum(damage_taken)
		into entity_health, entity_damage
	from entity e 
		where entity_id = _entity_id or
		(is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = _entity_id
				));
	
			
	if entity_damage >= entity_health then
		call KillEntity(_entity_id);
	end if;
			
			
end //
delimiter ; 

drop procedure if exists KillEntity;
delimiter //
create procedure KillEntity(in pEntityId int)
begin
	
	
	
	declare _tileId int;
	declare _entityType varchar(50);
	declare _accountId int;



	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if pEntityId is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	



	select entity_type, tile_id
		into _entityType, _tileId
	from entity
		where entity_id = pEntityId;
	
	select _entityType, _tileId;
	if (_entityType = "monster") then
		set autocommit = off;
		start transaction;
			delete from entity where entity_id = pEntityId;
		commit;	
	
		-- call spawnmonsterchest(tile_id);
	
	elseif (_entityType = "player") then
		select account_id
			into _accountId
		from entity 
			where entity_id = pEntityId;
			
		set autocommit = off;
		start transaction;
			delete from entity where entity_id = pEntityId;
		commit;	
	
		
		call createplayer(_accountId);
	end if;

			
end //
delimiter ; 



drop procedure if exists PlayerWin;
delimiter //
create procedure PlayerWin(in playerId int)
begin
	
	
	declare _distance int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if playerId is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;


	select ceil(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)))
		into _distance
	from tile t 
		join entity e 
		on e.tile_id = t.tile_id 
		where e.entity_id = playerId;

	set autocommit = off;
	start transaction;

		update account
		join entity on entity.account_id = account.account_id 
		set account.highscore = if (_distance < account.highscore, account.highscore, _distance)
		where entity.entity_id = playerId;
	
	commit;

	call killentity(playerID);
			
end //
delimiter ; 



drop procedure if exists ResetGame;
delimiter //
create procedure ResetGame()
begin
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;


	set autocommit = off;
	start transaction;

		delete from entity;
		delete from tile;
		delete from message;
		update account set highscore = 0;
	
	
	commit;

	
	call GenerateMap(40, 40);
			
end //
delimiter ; 



drop procedure if exists RegenerateMap;
delimiter //
create procedure RegenerateMap()
begin
	
	
	
	declare _homeTile int;


	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

	set autocommit = off;
	start transaction;

		delete from entity where entity_type = 'monster' or entity_type = 'chest';
	
		delete from tile 
		where tile_type != 'inventory';
	
		delete tile 
		from tile
		join entity 
		on entity.entity_id = tile.owner_id 
		where entity.entity_type != 'player';
	
	
	commit;

	
	call GenerateMap(40, 40);


	select tile_id 
		into _homeTile
	from tile
		where x = 0 
		and y = 0 
		and tile_type = "ground";
	
	
	update entity
	set tile_id = _homeTile
	where entity_type = 'player';
			
end //
delimiter ; 



drop procedure if exists DeleteAccount;
delimiter //
create procedure DeleteAccount(in _account_id int)
begin
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _account_id is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;

	
	
	
	set autocommit = off;
	start transaction;
		select _account_id;
		delete from account 
		where account_id = _account_id;
	
	
	commit;

end //
delimiter ; 



drop procedure if exists MovePlayerHome;
delimiter //
create procedure MovePlayerHome(in _username varchar(50))
begin
	
	
	declare _homeTile int;
	
	declare exit handler for sqlexception
		begin
			
		get diagnostics condition 1
			@sqlstate = returned_sqlstate, 
			@errornumber = mysql_errno,
			@text = message_text;
	
		rollback;
	
		if @sqlstate = "45000" then
			resignal;
		else
			set @full_error = concat("Error: ", @text);
			signal sqlstate '45000'
				set message_text = @full_error, mysql_errno = @errornumber;
		end if;
	end;

 	if _username is null
		then signal sqlstate '45000'
			set message_text = 'Error: One or multiple of the arguments were null or invalid.', mysql_errno = 1000;
	end if;



	select tile_id 
		into _homeTile
	from tile
		where x = 0 
		and y = 0 
		and tile_type = "ground";

	set autocommit = off;
	start transaction;

		update entity 
		join account 
			on entity.account_id = account.account_id 
		set entity.tile_id = _homeTile
		where account.username = _username;
	
	
	commit;

end //
delimiter ; 


-- Below is used for the Create Database Task

call CreateDatabase();
call InsertData();