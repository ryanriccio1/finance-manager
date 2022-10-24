# CSC-222 Team Project

## Assignment 16: PoC

### Proof-of-Concept REST API integration in C#.

Uses the NuGET Package _RestSharp_ to perform a GET request with a [Chuck Norris API](https://api.chucknorris.io/) to pull a Chuck Norris joke from an externally hosted server.

### Technical Details

No query is required to get data from the API.</br>
Data is given in the format of:

```json
{
    "icon_url": "https://assets.chucknorris.host/img/avatar/chuck-norris.png",
    "id": "qo4THZdeRLypW5HqXueEqg",
    "url": "",
    "value": "If Chuck Norris was going to die in 4 seconds Steven Seagal \"Might\" get a hit on him with Steven Seagal dieing on Chuck Norris last breath"
}
```
