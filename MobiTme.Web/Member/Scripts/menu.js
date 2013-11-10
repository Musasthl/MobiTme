// The default tile setup offered to new users.
window.DefaultTiles = [
    {
        name: "Section1",
        tiles: [
           {
               id: "flickr1",
               name: "flickr",
               color: "bg-color-blue",
               label: "Weather",
               appTitle: "Weather App",
               appUrl: "http://www.bbc.co.uk/weather/",
               size: "tile-double",
               iconSrc: "",
               cssSrc: ["tiles/weather/weather.css"],
               scriptSrc: ["tiles/weather/jQuery.simpleWeather.js", "tiles/weather/weather.js"],
               initFunc: "load_weather",
               initParams: {
                   location: 'London, UK'
               },
               appInNewWindow: true
           },
           {
                id: "news1",
                name: "news",
                color: "bg-color-yellow",
                label: "Amazon",
                appTitle: "Amazon",
                appUrl: "http://www.bbc.co.uk/weather/",
                size: "tile-double-vertical",
                iconSrc: "img/Amazon alt.png",
                cssSrc: [],
                scriptSrc: [],
                initFunc: "",
                initParams: {},
                appInNewWindow: true
            },
            {
                id: "maps",
                name: "news",
                color: "bg-color-purple",
                label: "Amazon",
                appTitle: "Amazon",
                appUrl: "http://www.bbc.co.uk/weather/",
                size: "tile-triple tile-double-vertical",
                iconSrc: "img/Google Maps.png",
                cssSrc: [],
                scriptSrc: [],
                initFunc: "",
                initParams: {},
                appInNewWindow: true
            } 
        ];

// Convert it to a serialized string
window.DefaultTiles = _.map(window.DefaultTiles, function (section) {
    return "" + section.name + "~" + (_.map(section.tiles, function (tile) {
        return "" + tile.id + "," + tile.name;
    })).join(".");
}).join("|");
