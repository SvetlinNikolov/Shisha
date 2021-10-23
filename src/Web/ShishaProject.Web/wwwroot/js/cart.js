// Cart JS logic
// Variable definitions
let allPackagingButtons = document.querySelectorAll('.cart-container .cart-item .packaging-choices-container button');
let allRemoveButtons = document.querySelectorAll('.cart-container .cart-item .cart-item-remove-icon-container');
let cartOrderButton = document.getElementById('cart-oder-button');

// Function definitions
function changePackaging() {
    // Get elements
    let packagingChoices = this.parentNode.childNodes;
    let currentPackaging;
    let newPackaging = this;

    for (let packagingChoice of packagingChoices) {
        if (packagingChoice.classList) {
            if (packagingChoice.classList.contains('active')) {
                currentPackaging = packagingChoice;
                break;
            }
        }
    }

    // Set 
    currentPackaging.classList.remove('active');
    newPackaging.classList.add('active');
}

function removeItem() {
    this.parentNode.remove();
        
    // TO DO
    // Also remove from localstorage
    // Also send a remove request to the cart saved on BE
}

function placeOrder() {
    let allCartItems = document.querySelectorAll('.cart-container .cart-item');
    let data = {
        cartItems: []
    };

    // Get the data from all cart items
    for (let item of allCartItems) {
        // Get the id
        let id = item.attributes[1].value;

        // Get the packaging
        let packagingContainer = checkChildrenForClass(item, 'packaging-choices-container');
        let packaging = checkChildrenForClass(packagingContainer, 'active').innerText;
        
        // Get the quantity
        // let quantity;
        let quantity = checkChildrenForClass(item, 'quantity').children[2].value;

        // Combine the data
        let currentItemData = {
            id,
            packaging,
            quantity
        }

        // Add the data to the data object
        data.cartItems.push(currentItemData);
    }

    // TO DO
    console.log(data);
    // SEND TO BE
    // Also remove from localstorage
    // Redirect user or show him a message?
}

function checkChildrenForClass(parentContainer, neededClass) {
    for (let currentChildItem of parentContainer.children) {
        if (currentChildItem.classList.contains(neededClass)) {
            return currentChildItem;
        }

        let checkChildrenOfChildren = checkChildrenForClass(currentChildItem, neededClass);

        if (checkChildrenOfChildren) {
            return checkChildrenOfChildren;
        }
    }
}

function checkScrollPosition() {
    if (window.innerWidth > 800) {
        let scrollCurrent = window.scrollY;
        let cartContainer = document.getElementById('cart-container');
        let cartContainerOffset = cartContainer.offsetTop;
        let cartTotalPriceContainer = document.getElementById('cart-item-total-price-container');

        if (scrollCurrent + 40 >= cartContainerOffset) {
            cartTotalPriceContainer.classList.add('sticky-cart-total');
            cartContainer.classList.add('sticky-cart-total');
        }
    }
}

// Add event listeners
// Packaging selection
for (let packagingBtn of allPackagingButtons) {
    packagingBtn.addEventListener('click', changePackaging);
}

// Item remove
for (let removeBtn of allRemoveButtons) {
    removeBtn.addEventListener('click', removeItem);
}

// Place order
cartOrderButton.addEventListener('click', placeOrder);

// Track the window scroll for the cart container
window.addEventListener('scroll', checkScrollPosition);