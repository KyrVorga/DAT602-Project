use `battlespire`;

select * from account;
select * from entity;
select * from entity where entity_type = "player" and account_id = 11;



call ResetGame();
call getplayerbyaccusername("asdf") 
call CreatePlayer(11)
call deleteaccount(1) 




call createAccount('Rhylei','Rhylei1@aureate.dev', '');



update account 
	set attempts  = attempts + 1 
	where username = 'asdf'




select  a.account_id 
		from account a 
			join entity e
			on e.account_id = a.account_id 
			where a.username = 'asdf' ;

select 	
	select count(*) from tile
	join entity e 
	on tile.owner_id = e.entity_id 
	where entity_type = 'player'
delete tile 
		from tile
		join entity 
		on entity.entity_id = tile.owner_id 
		where entity.entity_type != 'player'
		;


	select entity_id, a.account_id 
	from entity e 
		join account a on a.account_id = e.account_id 
		where a.username = "asdf";

	if playerId = null then
		call createPlayer(accountId);
		select *
		from entity e 
			join account a on a.account_id = e.account_id 
			where a.username = _username;
	else
		
		select *
		from entity e 
			join account a on a.account_id = e.account_id 
			where a.username = _username;
	end if;


call isAdminAccount('asdf')

select * from entity where entity_type = "player";
select * from entity where entity_type = "monster";
select * from entity where entity_type = "item" and owner_id = 88;
select * from entity where entity_type = "chest";
select * from tile where x = 8 and y > 0 and tile_type != 'inventory' order by y;
select count(*) from tile where tile_type = 'exit'
select count(*) from tile where tile_type = 'wall'
select count(*) from tile where tile_type = 'ground'
select * from entity where entity_id = 89;

select entity_type, tile_id
	from entity
		where entity_id = 90;

	
	delete from entity 
	where account_id = 11

call MoveMonsterNPC(80);

call GetTilesByPlayer(82, 10, 10);
update entity set tile_id = 3442 where entity_id = 89;
update entity set attack = 10 where entity_id = 93;

call GetEntityInventoryTiles(1)


select sum(health), sum(damage_taken)
	from entity e 
		where entity_id = 93 or
		(is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = 93
				));
			
			
			delete from entity where entity_id = 92
call killentity(93) 

call GetPlayerStats();


call checkentitystatus(92) 


	call CreateItem(91);

select * from entity e where entity_id = 87

update entity 
set attack = attack + 2
where entity_id = 90

		select sum(health), sum(attack), sum(defense)
	from entity e 
		where e.entity_id =	78
		or (is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = 78
				));

			
	select sum(health), sum(attack), sum(defense), sum(defense), sum(damage_taken)
	from entity e 
		where entity_id = 94 or
		(is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = 94
				))
			
	select sum(health)
	from entity e 
		where entity_id = 93 or
		(is_equipped = true
		and e.tile_id in (
				select t2.tile_id
				from tile t2
					join entity e 
						on t2.owner_id = e.entity_id 
					join entity e2 
						on e2.tile_id = t2.tile_id 
					where e.entity_id = 93
				));

select t2.tile_id from tile t2 where t2.owner_id = 86 and t2.tile_id not in (
	select t.tile_id
	from tile t
		join entity e 
			on t.owner_id = e.entity_id 
		join entity e2 
			on e2.tile_id = t.tile_id 
		where e.entity_id = 86
	)



call GetEntityInventory(308)

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
	select *
	from entity e 
	join account a on a.account_id = e.account_id 
	where a.username = "asdf";
	
	
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
