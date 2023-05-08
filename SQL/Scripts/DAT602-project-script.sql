drop database if exists `battlespire`;
create database if not exists `battlespire`;
use `battlespire`;

drop procedure if exists CreateDatabase;
delimiter //
create procedure CreateDatabase()
begin
	drop table if exists account;
	create table if not exists account (
		account_id integer not null auto_increment,
		username varchar(50) not null unique,
		email varchar(100) not null unique,
		password varchar(50) not null,
		attempts integer not null default 0,
		is_locked boolean not null default 0,
		is_logged_in boolean not null default 0,
		is_administrator boolean not null default 0,
		highscore integer not null default 0,
		constraint pk_account primary key (account_id)
	);

	drop table if exists message;
	create table if not exists message (
		message_id integer not null auto_increment,
		account_id integer not null,
		message varchar(500) not null,
		sent_time timestamp not null,
		constraint pk_message primary key (message_id),
		constraint fk_message_account foreign key (account_id)
		references account(account_id) on delete cascade
	);

	drop table if exists entity_type;
	create table if not exists entity_type (
		entity_type varchar(50) not null,
		constraint pk_entity_type primary key (entity_type)
	);

	drop table if exists tile_type;
	create table if not exists tile_type (
		tile_type varchar(50) not null,
		constraint pk_tile_type primary key (tile_type)
	);

	drop table if exists tile;
	create table if not exists tile (
		tile_id integer not null auto_increment,
		x integer not null,
		y integer not null,
		tile_type varchar(50) not null,
		owner_id integer,
		constraint pk_tile primary key (tile_id),
		constraint fk_tile_tile_type foreign key (tile_type)
		references tile_type(tile_type) on delete cascade
	);

	drop table if exists entity;
	create table if not exists entity (
		entity_id integer not null auto_increment,
		name varchar(50),
		health integer,
		current_health integer,
		attack integer,
		defense integer,
		healing integer,
		account_id integer,
		entity_type varchar(50) not null,
		tile_id integer,
		owner_id integer,
		killscore integer,
		inventory_used tinyint,
		is_equipped boolean,
		constraint pk_entity primary key (entity_id),
		constraint fk_entity_account foreign key (account_id)
		references account(account_id) on delete cascade,
		constraint fk_entity_entity_type foreign key (entity_type)
		references entity_type(entity_type) on delete cascade,
		constraint fk_entity_tile foreign key (tile_id)
		references tile(tile_id) on delete cascade,
		constraint fk_entity_owner foreign key (owner_id)
		references entity(entity_id) on delete cascade
	);

	alter table tile
	add constraint fk_tile_entity foreign key (owner_id)
		references entity(entity_id) on delete cascade;
end //
delimiter ;




drop procedure if exists InsertData;
delimiter //
create procedure InsertData()
begin
    
    -- create entity types
    insert into entity_type (entity_type)
    values 
		("player"),
		("item"),
		("monster"),
		("chest");
    
    -- create terrain types
    insert into tile_type (tile_type)
    values 
		("ground"),
		("wall"),
		("inventory"),
		("equipped_armor"),
		("equipped_sword"),
		("equipped_shield"),
		("equipped_amulet");
    
	call GenerateMap(40, 40);


	-- add some tiles that are further from the center manually
	insert into tile (x, y, tile_type)
	values (100, 250, 'ground');
	
	insert into tile (x, y, tile_type)
	values (473, 763, 'ground');
	
	insert into tile (x, y, tile_type)
	values (1354, 3581, 'ground');
	
	insert into tile (x, y, tile_type)
	values (13354, 35581, 'ground');



    insert into account (username, email, password, highscore)
    values 
        ("Sevro", "Sevro@howler.com", "omnis_vir_lupus", 4890),
        ("Screwface", "Screwface@howler.com", "omnis_vir_lupus", 2630),
        ("Pebble", "Pebble@howler.com", "omnis_vir_lupus", 1332),
        ("Clown", "Clown@howler.com", "omnis_vir_lupus", 1150),
        ("Thistle", "Thistle@howler.com", "omnis_vir_lupus", 879),
        ("Harpy", "Harpy@howler.com", "omnis_vir_lupus", 621),
        ("Weed", "Weed@howler.com", "omnis_vir_lupus", 482),
        ("Quinn", "Quinn@howler.com", "omnis_vir_lupus", 263),
        ("Rotback", "Rotback@howler.com", "omnis_vir_lupus", 158);
        
	insert into account (username, email, password, is_administrator)
    values 
		('KyrVorga', 'kyrvorga@mail.com', 'omnis-vir-lupus', true),
		('Todd', 'todd@nmit.ac.nz', '1234', true);
    

	-- these 4 use the id's that are manually added to provied some higher tier items.
	call SpawnChest(6562);
	call SpawnChest(6563);
	call SpawnChest(6564);
	call SpawnChest(6565);

	call SpawnChest(1523);
	call SpawnChest(2351);
	call SpawnChest(6431);
	call SpawnChest(3461);
	call SpawnChest(134);
	call SpawnChest(4621);
	call SpawnChest(1200);
	call SpawnChest(1341);
	call SpawnChest(1342);
	call SpawnChest(1343);
	call SpawnChest(1344);
	call SpawnChest(1345);
	call SpawnChest(1346);
	call SpawnChest(1347);
	call SpawnChest(1348);
	call SpawnChest(1349);
	call SpawnChest(1340);
	call SpawnChest(1332);
	call SpawnChest(1334);
    
	
	call SpawnMonster(6500); 
	call SpawnMonster(6500); 
	call SpawnMonster(6500); 
	call SpawnMonster(6500); 
	
	call SpawnMonster(6562); 
	call SpawnMonster(6562); 
	call SpawnMonster(6562); 
	call SpawnMonster(6562); 
	
	call SpawnMonster(6563);
	call SpawnMonster(6563);
	call SpawnMonster(6563);
	call SpawnMonster(6563);
	
	call SpawnMonster(6564); 
	call SpawnMonster(6564); 
	call SpawnMonster(6564); 
	call SpawnMonster(6564); 
	
	call SpawnMonster(6565); 
	call SpawnMonster(6565); 
	call SpawnMonster(6565); 
	call SpawnMonster(6565); 



	call CreatePlayer(1);
	call CreatePlayer(2);
	call CreatePlayer(3);
	call CreatePlayer(4);
	call CreatePlayer(5);
	call CreatePlayer(6);
	call CreatePlayer(7);
	call CreatePlayer(8);
	call CreatePlayer(9);
	
  
	call SendMessage(1, "Hello ****heads!");
	call SendMessage(2, "Hey!");
	call SendMessage(3, "Ya-hallo!");
	call SendMessage(4, "Osu~");
	call SendMessage(5, "Sup.");
	call SendMessage(6, "Woo-hoo!!!");
	call SendMessage(7, "Eyyyy");
	call SendMessage(8, "Bow before me!");
	call SendMessage(9, "*sneaking*");

end //
delimiter ;

        
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

	set _origin_tile_owner = GetEntityIDFromInventoryTile(_origin_tile_id);
	set _target_tile_owner = GetEntityIDFromInventoryTile(_target_tile_id);

	
	update entity 
	set tile_id = _target_tile_id,
		owner_id = _target_tile_owner
	where tile_id = _origin_tile_id;
	
	-- find owners of origin and target tiles and update.
	call UpdateEntityInventory(_origin_tile_owner);
	call UpdateEntityInventory(_target_tile_owner);

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



call CreateDatabase();
call InsertData();