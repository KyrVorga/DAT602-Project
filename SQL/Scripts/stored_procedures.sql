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

	case
		when _email in (
			select email
			from account
		)
		then select 'Error' as message;
		when _username in (
			select username
			from account
		)
		then select 'Error' as message;
		else insert into account (username, email, password)
	    values (_username, _email, _password);
	end case;

	select 'User Created' as message;
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
		if (
			select attempts
			from account
			where username = _username
		) >= 5
		then
			select 'Error' as message;
			leave login;
		end if;
		
		if (
			select username
			from account
			where username = _username
			and password = _password
		) = _username
		then
			select 'Login succesful.' as message;
		else
			select 'Error' as message;
		end if;

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

-- lock table and start a transaction 
	while _x <= _width do
		while _y <= _height do
			insert into tile (x, y, tile_type)
	        values (_x, _y, "ground");
			set _y = _y + 1;
		end while;
	    set _y = _height * -1;
	    set _x = _x + 1;
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
-- lock table and start a transaction 
	while _x <= _width do
		while _y <= _height do
			insert into tile (x, y, tile_type, owner_id)
	        values (_x, _y, "inventory", _entity_id);
			set _y = _y + 1;
		end while;
	    set _y = _height * 0;
	    set _x = _x + 1;
	end while;

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
			current_health,
			attack,
			defense,
			healing,
			tile_id,
			killscore,
			inventory_used
		)
		values (
			_account_id,
			"player",
			1,
			1,
			1,
			1,
			1,
			_home_tile,
			0,
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
	
		
	set autocommit = off;
	start transaction;


		update entity 
		set tile_id = _target_tile_id
		where tile_id = _origin_tile_id
		and entity_id = _item_id;

	
	commit;

end //
delimiter ;





--   _   _          _      _         ___     _   _ _          ___                 _                
--  | | | |_ __  __| |__ _| |_ ___  | __|_ _| |_(_) |_ _  _  |_ _|_ ___ _____ _ _| |_ ___ _ _ _  _ 
--  | |_| | '_ \/ _` / _` |  _/ -_) | _|| ' \  _| |  _| || |  | || ' \ V / -_) ' \  _/ _ \ '_| || |
--   \___/| .__/\__,_\__,_|\__\___| |___|_||_\__|_|\__|\_, | |___|_||_\_/\___|_||_\__\___/_|  \_, |
--        |_|                                          |__/                                   |__/ 
-- 
drop procedure if exists UpdateEntityInventory;
delimiter //
create procedure UpdateEntityInventory(in _entity_id int)
begin	
	
	update entity
	set inventory_used = (select count(entity_id) from (select * from entity) as e where owner_id = _entity_id)
	where entity_id = _entity_id;
	
end //
delimiter ;

-- THIS PROCEDURE IS INCOMPLETE!
-- drop procedure if exists PlayerEquipItem;
-- delimiter //
-- create procedure PlayerEquipItem(in _item_id int)
-- begin
-- 	
-- 	update entity 
-- 	set is_equipped = true
-- 	where entity_id = _item_id;
-- 
-- 	-- change the tile to the players respective equipment slot.
-- 		
-- end //
-- delimiter ;






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
	
	set autocommit = off;
	start transaction;

		select ceil(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)))
		into _distance
		from entity e 
		join tile t
			on t.tile_id = e.tile_id
		where
			e.entity_id = _chest_id;
	
	
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
		
		inventory_tile_loop: loop
			set _x = ceil(rand() *8);
			set _y = ceil(rand() *4);
			if (select tile_id from tile where x = _x and y = _y and tile_type = "inventory" and owner_id = _chest_id) != null
				then iterate inventory_tile_loop;
			end if;
			set _inventory_tile = (select tile_id from tile where x = _x and y = _y and tile_type = "inventory" and owner_id = _chest_id);
			leave inventory_tile_loop;	
		end loop inventory_tile_loop;
		
		-- create the new item
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


	insert into entity (entity_type, tile_id)
	values ("chest", _tile_id);

	set _chest_id =  GetChestIDFromTile(_tile_id);

	call GenerateInventory(_chest_id, 8, 4);

	set _item_total = ceil(rand() *2);

	while _items_created < _item_total do
		call CreateItem(_chest_id);
		set _items_created = _items_created + 1;
	end while;

	call UpdateEntityInventory(_chest_id);
	
	-- might need to return the entity_id into an out variable or as a select
end //
delimiter ;



--   ___                          __  __             _              ___ _           _   
--  / __|_ __  __ ___ __ ___ _   |  \/  |___ _ _  __| |_ ___ _ _   / __| |_  ___ __| |_ 
--  \__ \ '_ \/ _` \ V  V / ' \  | |\/| / _ \ ' \(_-<  _/ -_) '_| | (__| ' \/ -_|_-<  _|
--  |___/ .__/\__,_|\_/\_/|_||_| |_|  |_\___/_||_/__/\__\___|_|    \___|_||_\___/__/\__|
--      |_|                                                                             
-- 
drop procedure if exists SpawnMonsterChest;
delimiter //
create procedure SpawnMonsterChest(in _tile_id int)
begin
	
	declare _item_total int;
	
	insert into entity (entity_type, tile_id)
	values ("chest", _tile_id);
	

	set _item_total = ceil(rand() *5);
	-- might need to return the entity_id into an out variable or as a select
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
	declare _current_health int;
	
	declare _type_mod int ;
	declare _monster_type varchar(50) default "Goblin ";
	declare _tier varchar(10) default "I";
	
	-- find the distance of the chest from the center
	declare _distance int;
	select ceil(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)))
	into _distance
	from tile t 
	where tile_id = _tile_id;


	set _type_mod = ceil(rand() *4);
	set _health = pow(_distance, 1.25);
	set _attack = pow(_distance, 1.25) / 5;
	set _defense = pow(_distance, 1.25) / 5;
	set _healing = pow(_distance, 1.25) / 10;
	set _current_health = ceil((rand() * (1.3 - 0.7) + 0.7) * _health);


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
	

	-- create the new monster
	insert into entity (name, health, current_health, attack, defense, healing, entity_type, tile_id)
	values (
		concat(_monster_type, _tier),
		_current_health,
		_current_health,
		ceil((rand() * (1.3 - 0.7) + 0.7) * _attack),
		ceil((rand() * (1.3 - 0.7) + 0.7) * _defense),
		ceil((rand() * (1.3 - 0.7) + 0.7) * _healing),
		"monster",
		_tile_id
	);
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
	
	select username
	from account;
	
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
	
	select concat(username, ': ', highscore)
	from account;
	
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
	
	select concat('<',time(m.sent_time),'> ',a.username,': ', message) as message
	from message m
	join account a
	on m.account_id = a.account_id;
	
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
	
insert into message (account_id, message, sent_time)
values (_account_id, _message, current_timestamp());	
	
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
		y <= _y + _height 
	order by x desc, y asc;

end //
delimiter ;



   
drop procedure if exists GetAllEntities; -- excluding items
delimiter //
create procedure GetAllEntities()
begin
	
	select *
	from entity e 
	where e.entity_type != "item";

	
end //
delimiter ;  

drop procedure if exists GetPlayerByAccUsername; -- excluding items
delimiter //
create procedure GetPlayerByAccUsername(in _username varchar(50))
begin
	
	select *
	from entity e 
	join account a on a.account_id = e.account_id 
	where a.username = _username;

	
end //
delimiter ;  

drop procedure if exists MovePlayer; -- excluding items
delimiter //
create procedure MovePlayer(in _target_tile int, in _player_id int)
begin
	
		update entity 
		set tile_id = _target_tile
		where entity_id = _player_id;

	
end //
delimiter ; 



drop procedure if exists GetEntityInventory; -- excluding items
delimiter //
create procedure GetEntityInventory(in _entity_id int)
begin
	
		
	select * 
	from entity 
	where entity_type = "item"
	and owner_id = _entity_id;

	
end //
delimiter ; 

drop procedure if exists GetEntityInventoryTiles; -- excluding items
delimiter //
create procedure GetEntityInventoryTiles(in _entity_id int)
begin
	
		
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


-- Below is used for the Create Database Task

call CreateDatabase();
call InsertData();