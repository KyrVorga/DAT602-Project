use `battlespire`;
   

--    ___     _     ___ _                     ___ ___  
--   / __|___| |_  | _ \ |__ _ _  _ ___ _ _  |_ _|   \ 
--  | (_ / -_)  _| |  _/ / _` | || / -_) '_|  | || |) |
--   \___\___|\__| |_| |_\__,_|\_, \___|_|   |___|___/ 
--                             |__/                    
-- 
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






--    ___     _     ___     _   _ _          ___ ___    ___                ___                 _                  _____ _ _     
--   / __|___| |_  | __|_ _| |_(_) |_ _  _  |_ _|   \  | __| _ ___ _ __   |_ _|_ ___ _____ _ _| |_ ___ _ _ _  _  |_   _(_) |___ 
--  | (_ / -_)  _| | _|| ' \  _| |  _| || |  | || |) | | _| '_/ _ \ '  \   | || ' \ V / -_) ' \  _/ _ \ '_| || |   | | | | / -_)
--   \___\___|\__| |___|_||_\__|_|\__|\_, | |___|___/  |_||_| \___/_|_|_| |___|_||_\_/\___|_||_\__\___/_|  \_, |   |_| |_|_\___|
--                                    |__/                                                                 |__/                 
-- 
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






--    ___     _      ___ _           _     ___ ___    ___                _____ _ _     
--   / __|___| |_   / __| |_  ___ __| |_  |_ _|   \  | __| _ ___ _ __   |_   _(_) |___ 
--  | (_ / -_)  _| | (__| ' \/ -_|_-<  _|  | || |) | | _| '_/ _ \ '  \    | | | | / -_)
--   \___\___|\__|  \___|_||_\___/__/\__| |___|___/  |_||_| \___/_|_|_|   |_| |_|_\___|
--                                                                                     
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






--    ___     _      ___ _           _     ___ ___    ___                ___ _             
--   / __|___| |_   / __| |_  ___ __| |_  |_ _|   \  | __| _ ___ _ __   |_ _| |_ ___ _ __  
--  | (_ / -_)  _| | (__| ' \/ -_|_-<  _|  | || |) | | _| '_/ _ \ '  \   | ||  _/ -_) '  \ 
--   \___\___|\__|  \___|_||_\___/__/\__| |___|___/  |_||_| \___/_|_|_| |___|\__\___|_|_|_|
--                                                                                         
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



--    ___      _         _      _         _____ _         
--   / __|__ _| |__ _  _| |__ _| |_ ___  |_   _(_)___ _ _ 
--  | (__/ _` | / _| || | / _` |  _/ -_)   | | | / -_) '_|
--   \___\__,_|_\__|\_,_|_\__,_|\__\___|   |_| |_\___|_|  
--                                                        
drop function if exists CalculateTier;
delimiter //
create function CalculateTier(_distance int)
returns varchar(10) deterministic
begin
	declare _tier varchar(10);
	if pow(_distance, 1.25) < 488.28125 then
		set _tier = "I";
	end if;
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
