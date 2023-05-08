-- 
-- select * from entity e;
-- 
-- select * from tile t 
-- join entity e on e.tile_id = t.tile_id 
-- where entity_id = 80;


drop procedure if exists GetTilesByPlayer;
delimiter //
create procedure GetTilesByPlayer(in _player_id int, in _viewport_width int, in _viewport_height int)
begin
	
	declare _x int;
	declare _y int;
	declare _width int;
	declare _height int;

	set _width = _viewport_width /2 -1;
	set _height = _viewport_height /2 -1;
	
	select x,y
	into _x,_y	
	from tile t 
	join entity e on e.tile_id = t.tile_id 
	where entity_id = _player_id;

	select x,y, tile_type 
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
	order by x,y;

end //
delimiter ;

call GetTilesByPlayer(80,10,10);

