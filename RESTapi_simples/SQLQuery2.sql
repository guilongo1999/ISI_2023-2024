create procedure procedure_getUser
as
begin
select  [ID],[Username],[passwordHash],[passwordSalt] from UserInfo
end
exec procedure_getUser