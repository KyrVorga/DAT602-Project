use battlespire;

drop role if exists administrator;
create role administrator;
grant 
	all privileges
on battlespire.*
to administrator;


drop role if exists standard;
create role standard;
grant 
	execute
on battlespire.*
to standard;



drop user if exists testuser@localhost;
create user if not exists testuser@localhost
identified by 'test123';


grant standard
to testuser@localhost;
