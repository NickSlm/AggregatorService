**Game Data Snapshot Worker Service**

A .NET Windows Worker Service that periodically pulls game character data from the Blizzard API, stores it as versioned snapshots, and automatically removes outdated records.

**Overview**

This service runs two scheduled background workers:

**Worker 1 – Data Ingestion**

- Pulls character data from the Blizzard API

- Cleans and normalizes the data

- Creates a new snapshot record

- Saves all related character data linked to that snapshot

Each snapshot represents the full dataset at a specific point in time, enabling historical tracking.

**Worker 2 – Data Cleanup**

- Runs on schedule

- Deletes snapshots older than 100 days
  
- Removes related records to maintain database performance
