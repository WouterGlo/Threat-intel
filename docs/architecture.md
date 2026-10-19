# ADR 0001: Store CVE data locally instead of querying the NVD live

## Status
Accepted

## Context
The dashboard needs search, filtering, sorting, pagination, KEV status,
inventory links and alerts. The NVD API is rate-limited and cannot join
with KEV data or with our own tables.

## Decision
A background worker syncs CVE and KEV data on a schedule into PostgreSQL.
The API and dashboard read only from our database.

## Consequences
- Fast, filterable queries and continued operation if the NVD is down
- Change detection for alerts becomes possible
- We must handle sync failures, deduplication and stale data
- Data can be up to one sync interval out of date, shown to the user