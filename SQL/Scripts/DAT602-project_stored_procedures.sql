use battlespire;
        
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


drop function if exists GetPlayerID;
delimiter //
create function GetPlayerID(_account_id int)
returns int deterministic
begin
	declare _player_id int;
	
	select e.entity_id
	into _player_id
	from entity e
	where e.entity_type = "player"
	and e.account_id = _account_id;

	return _player_id;
end //
delimiter ;


drop function if exists GetEntityIDFromInventoryTile;
delimiter //
create function GetEntityIDFromInventoryTile(_tile_id int)
returns int deterministic
begin
	declare _entity_id int;
	
	select owner_id
	into _entity_id
	from entity
	where tile_id = _tile_id;

	return _entity_id;
end //
delimiter ;



drop function if exists GetChestIDFromTile;
delimiter //
create function GetChestIDFromTile(_tile_id int)
returns int deterministic
begin
	declare _chest_id int;
	
	select entity_id
	into _chest_id
	from entity
	where entity_type = "chest"
	and tile_id = _tile_id;

	return _chest_id;
end //
delimiter ;


drop function if exists GetChestIDFromItem;
delimiter //
create function GetChestIDFromItem(_item_id int)
returns int deterministic
begin
	declare _chest_id int;
	
	select e.owner_id
	into _chest_id
	from entity e
	where e.entity_type = "item"
	and e.entity_id = _item_id;

	return _chest_id;
end //
delimiter ;


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



-- procedure should take an account_id and an item_id, it should change the owner_id,
-- then update the inventory_used of the player and the chest the item came from.
-- should allow players to equip item from chest directly.
drop procedure if exists MoveInventoryItem;
delimiter //
create procedure MoveInventoryItem(in _origin_tile_id int, in _target_tile_id int)
begin
	
	declare _origin_tile_owner int;
	declare _target_tile_owner int;
		
	set autocommit = off;
	start transaction;

		set _origin_tile_owner = GetEntityIDFromInventoryTile(_origin_tile_id);
		set _target_tile_owner = GetEntityIDFromInventoryTile(_target_tile_id);

		update entity 
		set tile_id = _target_tile_id,
			owner_id = _target_tile_owner
		where tile_id = _origin_tile_id;

		-- find owners of origin and target tiles and update.
		call UpdateEntityInventory(_origin_tile_owner);
		call UpdateEntityInventory(_target_tile_owner);
	
	commit;

end //
delimiter ;



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



drop function if exists CalculateTier;
delimiter //
create function CalculateTier(_distance int)
returns varchar(10) deterministic
begin
	declare _tier varchar(10);
	
	if pow(_distance, 1.25) >= 488.28125 then
		set _tier = "II";
	end if;
	if pow(_distance, 1.25) >= 976.5625 then
		set _tier = "III";
	end if;
	if pow(_distance, 1.25) >= 1953.125 then
		set _tier = "IV";
	end if;
	if pow(_distance, 1.25) >= 3906.25 then
		set _tier = "V";
	end if;
	if pow(_distance, 1.25) >= 7812.5 then
		set _tier = "VI";
	end if;
	if pow(_distance, 1.25) >= 15625 then
		set _tier = "VII";
	end if;
	if pow(_distance, 1.25) >= 31250 then
		set _tier = "VIII";
	end if;
	if pow(_distance, 1.25) >= 62500 then
		set _tier = "IX";
	end if;
	if pow(_distance, 1.25) >= 125000 then
		set _tier = "X";
	end if;

	return _tier;
end //
delimiter ;

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
		insert into entity (name, health, attack, defense, healing, entity_type, owner_id, tile_id)
		values (
			concat(_item_type, _tier),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _health),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _attack),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _defense),
			ceil((rand() * (1.3 - 0.7) + 0.7) * _healing),
			"item",
			_chest_id,
			_inventory_tile
		);
	commit;
end //
delimiter ;



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
	declare _monster_type varchar(50) default "Goblin";
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


drop procedure if exists IsAdminAccount;
delimiter //
create procedure IsAdminAccount(in _username varchar(50))
begin
	
	select is_administrator 
	from account
	where username =_username;
	
end //
delimiter ;


drop procedure if exists GetAllPlayers;
delimiter //
create procedure GetAllPlayers()
begin
	
	select username
	from account;
	
end //
delimiter ;



drop procedure if exists GetLeaderboard;
delimiter //
create procedure GetLeaderboard()
begin
	
	select concat(username, ': ', highscore)
	from account;
	
end //
delimiter ;


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


drop procedure if exists SendMessage;
delimiter //
create procedure SendMessage(in _account_id int, in _message varchar(500))
begin
	
insert into message (account_id, message, sent_time)
values (_account_id, _message, current_timestamp());	
	
end //
delimiter ;


