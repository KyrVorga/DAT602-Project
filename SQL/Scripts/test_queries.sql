select * from account;
select * from entity;
select * from entity where entity_type = "player" and account_id = 4;


select * from entity where entity_type = "player";
select * from entity where entity_type = "monster";
select * from entity where entity_type = "item";
select * from entity where entity_type = "chest";

select * from tile where x >= 100;

select * from tile where tile_type = 'inventory';

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
