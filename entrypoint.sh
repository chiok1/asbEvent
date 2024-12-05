#!/bin/bash

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to become available
echo "Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1; do
    sleep 1
done

echo "SQL Server is up and running"

# Check if DROP_DATABASE is set to true
if [ "$DROP_DATABASE" = "true" ]; then
    echo "Dropping and recreating the database..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF EXISTS (SELECT name FROM sys.databases WHERE name = 'asb_event_dev') DROP DATABASE asb_event_dev;"
fi

# Run the initialization scripts
echo "Running initialization scripts..."
for script in /usr/src/app/init-db/*.sql; do
    echo "Executing $script..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i "$script"
done

echo "Initialization complete"


# Bring SQL Server back to the foreground
wait 

