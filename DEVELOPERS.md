### DEVELOPERS
resources a developer might need while developing on this project

## prepare environment
- install the asp.net group in VS 2017  
- `cd WgWall\ClientApp && npm install -g @angular/cli` to install angular command line tools

## Commands
`cd WgWall && dotnet watch run` to start server & listen for changes  
`cd WgWall\ClientApp && ng build --watch` to recompile frontend files automatically  
`cd WgWall.Migrations && dotnet ef migrations add MyMigrationName --startup-project ../WgWall/` to create a new migration called `MyMigrationName`  
`cd WgWall && dotnet ef database update` to execute any pending migrations