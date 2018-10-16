### DEVELOPERS
resources a developer might need while developing on this project

## Commands
`cd WgWall && dotnet watch run` to start server & listen for changes  
`cd WgWall\ClientApp && ng build --watch` to recompile frontend files automatically  
`cd WgWall.Migrations && dotnet ef migrations add InitialMigration --startup-project ../WgWall/` to create a new migration  
`cd WgWall && dotnet ef database update` to execute any pending migrations