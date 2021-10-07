// Products JS logic
// Variable definitions
let allInputs = document.querySelectorAll('.filter-left-container input');
let selectDropdown = document.querySelector('.filters-top .filters-select select');
let pageNavigationButtons = document.querySelectorAll('.pagination button');
let filterMenuIcon = document.getElementById('filter-menu-button');

// Function definitions
function navigatePageNavigation(clickedElement) {
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
    updateProducts();
}

function updatePageNavigation() {
    // Check if the button is a previous or next button
    if (this.classList.contains('button-previous') || this.classList.contains('button-next')) {
        navigatePageNavigation(this);
        return;
    }

    // Remove the styling of the current page
    let currentPage = document.querySelector('.pagination button.active');
    currentPage.classList.remove('active');

    // Add the styling for the new selected page
    let newPage = this;
    newPage.classList.add('active');

    // Update the products to the new page
    updateProducts();
}

function updateProducts() {
    // Get selected page from page navigation
    let currentPageNumber = document.querySelector('.pagination button.active').innerHTML;

    // Get select current select dropdown value
    let selectValue = selectDropdown.value;

    // Get checked checkboxes
    let checkedInputsList = document.querySelectorAll('.filter-left-container input:checked');
    let checkedInputsArray = [];

    for (let checkedInput of checkedInputsList) {
        checkedInputsArray.push(checkedInput.id);
    }

    // TO DO - SEND TO THE BE
    let postData = { context: checkedInputsArray };

    $.ajax({
        url: "Products/GetFlavours",
        data: JSON.stringify(postData),
        contentType: "application/json; charset=utf-8",
        success: function (html) {
            alert("I am successfull")
        }
    });

    console.log('Current page: ', currentPageNumber);
    console.log('Selected checkboxes: ', checkedInputsArray);
    console.log('Select value: ', selectValue);
    // TO DO - SEND TO THE BE
}

function toggleFilterMenu() {
    let filterMenu = document.getElementById('filters-menu');

    if (filterMenu.style.display == 'flex') {
        filterMenu.style.display = 'none';
        return;
    }

    filterMenu.style.display = 'flex';
}

// Add event listeners
// On input (checkbox) click
for (let input of allInputs) {
    input.addEventListener('click', updateProducts);
}

// On select change
selectDropdown.addEventListener('change', updateProducts);

// On page change
for (let button of pageNavigationButtons) {
    button.addEventListener('click', updatePageNavigation);
}

// Show filters on button / header click
filterMenuIcon.addEventListener('click', toggleFilterMenu);
