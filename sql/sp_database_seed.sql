create or replace procedure sp_database_seed()
language plpgsql    
as $$
begin
    -- Delete records
	delete from public."user" where 1=1;
	delete from public.account where 1=1;
	delete from public."role" where 1=1;
	
    -- Role Seeder
    insert into public."role" (id, name, created_at) values (1, 'Administrator', '2023-08-28') ;

    -- Account Seeder
   	insert into public.account(id, username, password, salt, role_id, created_at) values (1, 'jp_1992@yopmail.com', '70FBBC0635B570D15BDBD33F0F8991430C3889E91D7AD6E417D7810A8B012A3C7F610B0FBA91F19B639906A8016D336A097CD78E77C0C898BB124F10A41F0853', 'D12B99B0BA7DBB12E2E4AE23A31998F9BBBCD0DF92F3DA5D07419FADEB30DE0713675F7F28C9424C70189A73DACC15D4E7AC683BFA9FE023C4BD42EF432463E5', 1, '2023-08-28');
	
	-- User Seeder
	
	insert into public."user"(id, firstname, lastname, account_id, created_at) values (1, 'Juan', 'Pinzon', 1, '2023-08-28');
    commit;
end;$$;