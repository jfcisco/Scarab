window.onload = function() {
    var animeList = [];
    var animeListElement = document.querySelector("#anime-list");

    // Fetch anime list
    fetch("/api/Anime/")
    .then(response => response.json())
    .then((response) => {
        animeListElement.innerHTML = animeListToHtml(response);
    });
}

function animeListToHtml(animeList) {
    return animeList.reduce((list, currentAnime) => {

        var animeMarkup = 
            `<a 
                id="anime-${currentAnime.id}"
                href="#" 
                class="list-group-item list-group-item-action"
                onclick="handleAnimeSelection(${currentAnime.id})">
                ${currentAnime.name}
            </a>`;

        return list + animeMarkup;
    }, "");
}

function handleAnimeSelection(id) {
    var requestUrl = "/api/Anime/" + id;
    var episodeNumber = document.querySelector(`input[name="episode"]`).value;

    if (episodeNumber !== null)
    {
        requestUrl += "/" + episodeNumber;
    }

    fetch(requestUrl);
}

function handlePlayButton() {
    fetch("/api/Anime/player", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify("PLAY")
    });
}

function handleFullScreenButton() {
    fetch("/api/Anime/player", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify("FULLSCREEN")
    });
}

function handleFormSubmit(event) {
    event.preventDefault();
    
}