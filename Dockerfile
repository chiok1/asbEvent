# Use the official SQL Server image
FROM mcr.microsoft.com/mssql/server:2022-latest

# Switch to root user to create the directory
USER root

# Set environment variables
ENV ACCEPT_EULA=Y
ENV A_PASSWORD=Qwerty123!  

# Create a directory for initialization scripts
# RUN mkdir -p /usr/src/app/init-db
RUN mkdir -p /usr/src/app/init-db && \
    chmod -R 755 /usr/src/app

# Copy initialization SQL scripts
COPY ./init-db/ /usr/src/app/init-db/

# Copy the custom entrypoint script
COPY ./entrypoint.sh /usr/src/app/entrypoint.sh

# Make the entrypoint script executable
RUN chmod +x /usr/src/app/entrypoint.sh

# Expose the default SQL Server port
EXPOSE 1433

# Set the custom entrypoint
ENTRYPOINT ["/usr/src/app/entrypoint.sh"]
