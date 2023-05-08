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

