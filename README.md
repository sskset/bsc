# Bowling Game Calculator

## Development
   * Visual Studio 2019

## Startup
    ![Startup](https://github.com/sskset/bsc/startup.png)

## Runtime
* POST to http://localhost:5000/api/scores
* Request payload samples
```json
{
    "pinsDowned": [int]
}
```

```json
{
    "pinsDowned": [10,10,10,10,10,10,10,10,10,10,10,10]
}
```  

* Response samples  

```json
{
    "frameProgressScores": ["30","60","90","120","150","180", "210", "240", "270", "300"],
    "gameCompleted": true,
}

```

by: SK