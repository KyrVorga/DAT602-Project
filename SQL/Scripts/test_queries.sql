use `battlespire`;

select * from account;
select * from entity;


select * from entity where entity_type = "player";
select * from entity where entity_type = "monster";
select * from entity where entity_type = "item" and owner_id = 1;
select * from entity where entity_type = "chest";

select * from entity where entity_id = 89;

call MoveMonsterNPC(80);

call GetTilesByPlayer(82, 10, 10);
update entity set tile_id = 3442 where entity_id = 89;
update entity set attack = 10 where entity_id = 93;

call GetEntityInventoryTiles(1)

select * from entity where entity_type = "player" and account_id = 11;

	call CreateItem(86);



	select t.tile_id, e.entity_id 
	from tile t
		join entity e 
			on t.owner_id = e.entity_id 
		where e.entity_id = 86
			and t.tile_id in (select t2.tile_id from tile t2 where t2.owner_id = 86);



call GetEntityInventory(88)

call GetEntityInventory(92)
call EquipItem(93, 94)

delete from entity where entity_id = 95

update entity set tile_id =8062 where entity_id = 95

select * from tile where x >= 100;

select * from tile where tile_type = 'inventory' and owner_id = 89;

	select ceil(pow(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)), 1.25))
	from entity e 
	join tile t on t.tile_id = e.tile_id
	where e.entity_id = 1;

-- ceil(pow(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)), 1.25))
-- above is an low exponential function


call CreateAccount("Pax", "pax@mars.net", "password");

show function status;
show procedure status;
call CreatePlayer(1) 

select * from entity e where e.entity_id = 54


call GetPlayerByAccUsername("asdf");

	
-- call GetAllEntities()
	
call moveplayer(3607, :_player_id) 
	
	
	
select pow(sqrt(pow(abs(t.x), 2) + pow(abs(t.x), 2)), 1.25)
from entity e 
join tile t on t.tile_id = e.tile_id
where e.entity_id = 40;

select (rand() * (1.3 - 0.7) + 0.7);


if (select tile_id from tile where x = _x and y = _y and tile_type = "inventory" and owner_id = _chest_id) != null
	then select "exists";
end if;


select concat('<',m.sent_time,'> ',a.username,': ', message) 
from message m
join account a
on m.account_id = a.account_id 

select concat(username, ': ', highscore)
from account;


	select is_administrator 
	from account
	where username = "KyrVorga";
