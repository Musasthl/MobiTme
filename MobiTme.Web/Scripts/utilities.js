function log(message)
{
    console.log(message);
}

function getFromQueryString(requestId) {
    // Method to get a value from a query string
    parameters = window.location.search.substring(1);
    toSearch = parameters.split("&");
    for (i = 0; i < toSearch.length; i++) {
        result = toSearch[i].split("=");
        if (result[0] == requestId) {
            return result[1];
        }
    }
}