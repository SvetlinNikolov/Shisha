// Search bar JS logic
// Variable definitions
let searchBarBtn = document.getElementById('search-bar-btn');
let searchBarField = document.getElementById('search-bar-field');

// Function definitions
function sendSearchBarData() {
    // TO DO - Send to the BE
    console.log(searchBarField.value);
    // postRequest('url', { data })
    // .then(data => {
    //     console.log('data', data);
    // });
    // TO DO - Do something with the data from the BE
}

// Add event listeners
searchBarBtn.addEventListener('click', sendSearchBarData);
