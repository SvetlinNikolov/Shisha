// Products JS logic
// Variable definitions
let allInputs = document.querySelectorAll('.filter-left-container input[type=checkbox]');
let selectDropdown = document.querySelector('.filters-top .filters-select select');
let filterMenuIcon = document.getElementById('filter-menu-button');
let customPriceFilterBtn = document.getElementById('filter-price-custom-button');
let checkBoxFilterPrice = document.querySelectorAll('.filter-left-container input[name="filter-price"]');
let checkBoxFilterStock = document.querySelectorAll('.filter-left-container input[name="filter-stock"]');
let allAddToCartDisabledButtons = document.querySelectorAll('.product-add-to-cart-container .product-add-to-cart-button-disabled');
let allAddToCartPopupCloseButtons = document.querySelectorAll('.product-add-to-cart-container .product-add-to-cart-popup-close');
let language = document.getElementById("page_language").innerText;

// Function definitions
function navigatePageNavigation(clickedElement) {
    // Get all buttons
    let pageNavigationButtons = document.querySelectorAll('.pagination button');

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

function updateProducts(searchTerm = '') {
    // Get selected page from page navigation
    let currentPageNumber = document.querySelector('.pagination button.active').innerHTML;

    // Get select current select dropdown value
    let selectValue = selectDropdown.value;

    // Get checked checkboxes
    let checkedInputsList = document.querySelectorAll('.filter-left-container input:checked');

    // Create variables for the request object
    // Price
    let price_from = null;
    let price_to = null;
    // Category
    let category_id = [];
    // Flavor
    let flavor = [];
    // Packaging
    let packaging = [];
    // Stock status
    let in_stock = 1;

    for (let checkedInput of checkedInputsList) {
        switch (checkedInput.name) {
            case 'filter-price':
                if (checkedInput.value === 'Custom') {
                    price_from = document.getElementById('filter-price-custom-from').value;
                    price_to = document.getElementById('filter-price-custom-to').value;

                    if (price_from.length < 1 || price_to.length < 1) {
                        return;
                    }
                    break;
                }

                let splitPriceValues = checkedInput.value.split(' - ');
                price_from = splitPriceValues[0];
                price_to = splitPriceValues[1];
                break;

            case 'filter-brand':
                category_id.push(checkedInput.id);
                break;

            case 'filter-flavor':
                flavor.push(checkedInput.id);
                break;

            case 'filter-packaging':
                packaging.push(checkedInput.id);
                break;

            case 'filter-stock':
                if (checkedInput.value === 'in-stock') {
                    in_stock = 1;
                    break;
                }

                in_stock = 0;
                break;
        }
    }

    let data = {
        'page': currentPageNumber,
        selectValue,
        price_from,
        price_to,
        category_id,
        svetlio: flavor,
        svetlio2: packaging,
        in_stock,
        language,
        'term': searchTerm
    }

    postRequestHTML('Products/GetFilteredFlavours', data).then(results => {
        let productsContainer = document.querySelector('.products');
        productsContainer.innerHTML = results;

        // Attach event listeners to the new DOM
        pageNavigation();
        packagingOptions();
    });
}

function toggleFilterMenu() {
    let filterMenu = document.getElementById('filters-menu');

    if (filterMenu.style.display == 'flex') {
        filterMenu.style.display = 'none';
        return;
    }

    filterMenu.style.display = 'flex';
}

function checkBoxToRadio(event, checkBoxArray) {
    let currentCheckBox = event.target;

    // Check if it's checked, in which case remove it
    if (currentCheckBox.classList.contains('checked-already')) {
        currentCheckBox.checked = false;
        currentCheckBox.classList.remove('checked-already');
        return
    }

    // Otherwise remove other checkboxes and check it
    for (let checkBox of checkBoxArray) {
        checkBox.checked = false;
        checkBox.classList.remove('checked-already');
    }

    currentCheckBox.checked = true;
    currentCheckBox.classList.add('checked-already');
};

function changePackaging() {
    // Get elements
    let parentContainer = this.parentNode;
    let packagingChoices = parentContainer.children;
    let currentPackaging;
    let newPackaging = this;
    let priceContainer = parentContainer.parentNode.parentNode.querySelector('.price-container .price-variants').children;
    let prevIndex;
    let newIndex;

    for (let packagingChoice of packagingChoices) {
        if (packagingChoice.classList) {
            if (packagingChoice.classList.contains('active')) {
                currentPackaging = packagingChoice;
                prevIndex = Array.prototype.indexOf.call(packagingChoices, packagingChoice);
                newIndex = Array.prototype.indexOf.call(packagingChoices, this);
                break;
            }
        }
    }

    // Set the price 
    priceContainer[prevIndex].classList.add('price-hidden');
    priceContainer[newIndex].classList.remove('price-hidden');

    // Set the packaging
    currentPackaging.classList.remove('active');
    newPackaging.classList.add('active');
}

function showAddToCartButtonPopup() {
    let parentContainer = this.parentNode;
    let popupContainer = parentContainer.querySelector('.product-add-to-cart-popup');

    // Remove the hidden class
    popupContainer.classList.remove('product-add-to-cart-popup-hidden');
}

function hideAddToCartButtonPopup() {
    let parentContainer = this.parentNode;

    // Remove the hidden class
    parentContainer.classList.add('product-add-to-cart-popup-hidden');
}

// Add event listeners
// On input (checkbox) click
for (let input of allInputs) {
    input.addEventListener('click', updateProducts);
}

// Custom price button click
customPriceFilterBtn.addEventListener('click', updateProducts)

// On select change
selectDropdown.addEventListener('change', updateProducts);

// Show filters on button / header click
filterMenuIcon.addEventListener('click', toggleFilterMenu);

// Price filter checkboxes
for (let checkBox of checkBoxFilterPrice) {
    checkBox.addEventListener('click', (event) => checkBoxToRadio(event, checkBoxFilterPrice));
}

// Stock filter checkboxes
for (let checkBox of checkBoxFilterStock) {
    checkBox.addEventListener('click', (event) => checkBoxToRadio(event, checkBoxFilterStock));
}

// Show the popup on button hover
for (let button of allAddToCartDisabledButtons) {
    button.addEventListener('mouseover', showAddToCartButtonPopup);
}

// Hide the popup on button hover
for (let button of allAddToCartPopupCloseButtons) {
    button.addEventListener('click', hideAddToCartButtonPopup);
}

// Reusable combined functions - definition and event listener
// Page navigation under the products
function pageNavigation() {
    // Define
    let pageNavigationButtons = document.querySelectorAll('.pagination button');

    // Set event listener
    for (let button of pageNavigationButtons) {
        button.addEventListener('click', updatePageNavigation);
    }
}
// Run
pageNavigation();

function packagingOptions() {
    // Define
    let allPackagingButtons = document.querySelectorAll('.product-text-and-price-container .packaging-choices-container button');

    // Set event listener
    for (let packagingBtn of allPackagingButtons) {
        packagingBtn.addEventListener('click', changePackaging);
    }
}
// Run
packagingOptions();


// Search bar JS logic
// Variable definitions
let searchBarBtn = document.getElementById('search-bar-btn');
let searchBarField = document.getElementById('search-bar-field');

// Function definitions
function sendSearchBarData() {
    updateProducts(searchBarField.value);
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

