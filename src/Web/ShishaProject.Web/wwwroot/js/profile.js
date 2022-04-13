// Profile JS logic
// Variable definitions
let profileDataBtn = document.getElementById('profile-info-container-btn');
let orderHistoryBtn = document.getElementById('profile-orders-container-btn');
let profileDataContent = document.getElementById('profile-info-container');
let orderHistoryContent = document.getElementById('profile-orders-container');
let language = document.getElementById('page_language').innerText;


// Function definitions
function changeProfileNav(event, changeTo) {
    // Change the content
    if (changeTo === 'profileData') {
        profileDataBtn.classList.add('active');
        orderHistoryBtn.classList.remove('active');

        profileDataContent.classList.add('profile-info-container-active');
        orderHistoryContent.classList.remove('profile-info-container-active');
        return;
    }

    profileDataBtn.classList.remove('active');
    orderHistoryBtn.classList.add('active');

    profileDataContent.classList.remove('profile-info-container-active');
    orderHistoryContent.classList.add('profile-info-container-active');
}

// Pagination
function navigatePageNavigation(clickedElement) {
    // Get all buttons
    let pageNavigationButtons = document.querySelectorAll('.profile-order-pagination button');

    // Define overwritable variables
    let currentActiveIndex;
    let indexToMakeActive;

    // Get the index of the active element
    pageNavigationButtons.forEach(function (currentValue, currentIndex) {
        if (currentValue.classList.contains('active')) {
            currentActiveIndex = currentIndex;
            return;
        }
    });

    // If previous, set to the previous index
    if (clickedElement.classList.contains('button-previous')) {
        indexToMakeActive = currentActiveIndex - 1;

        if (indexToMakeActive <= 1) {
            indexToMakeActive = 1;
        }
    }

    // If previous, set to the next index
    if (clickedElement.classList.contains('button-next')) {
        indexToMakeActive = currentActiveIndex + 1;

        if (indexToMakeActive >= pageNavigationButtons.length - 1) {
            indexToMakeActive = pageNavigationButtons.length - 2;
        }
    }

    // Set the stylings of the elements
    pageNavigationButtons[currentActiveIndex].classList.remove('active');
    pageNavigationButtons[indexToMakeActive].classList.add('active');

    // Update the products to the new page
    updateOrders();
}

function updatePageNavigation() {
    // Check if the button is a previous or next button
    if (this.classList.contains('button-previous') || this.classList.contains('button-next')) {
        navigatePageNavigation(this);
        return;
    }

    // Remove the styling of the current page
    let currentPage = document.querySelector('.profile-order-pagination button.active');
    currentPage.classList.remove('active');

    // Add the styling for the new selected page
    let newPage = this;
    newPage.classList.add('active');

    // Update the products to the new page
    updateOrders();
}

function updateOrders() {
    // Get selected page from page navigation
    let currentPageNumber = document.querySelector('.profile-order-pagination button.active').innerHTML;

    let data = {
        currentPageNumber,
        language
    }

    postRequestHTML('User/UserProfile', data).then(results => {
        let ordersContainer = document.querySelector('.profile-orders-container');
        ordersContainer.innerHTML = results;

        // Attach event listeners to the new DOM
        pageNavigation();
    });
}

// Add event listeners
profileDataBtn.addEventListener('click', (event) => changeProfileNav(event, 'profileData'));
orderHistoryBtn.addEventListener('click', (event) => changeProfileNav(event, 'orderHistory'));

// Reusable combined functions - definition and event listener
// Page navigation under the products
function pageNavigation() {
    // Define
    let pageNavigationButtons = document.querySelectorAll('.profile-order-pagination button');

    // Set event listener
    for (let button of pageNavigationButtons) {
        button.addEventListener('click', updatePageNavigation);
    }
}
// Run
pageNavigation();
