// Products JS logic
// Variable definitions
let allImageNavButtons = document.querySelectorAll('.product-image-and-text-container .product-image-container .product-image-navigation-container button');
let allPackagingButtons = document.querySelectorAll('.product-image-and-text-container .price-and-button-container .packaging-choices-container button');
let allRelatedProductNavButtons = document.querySelectorAll('.product-related-container .product-related-navigation button');

// Function definitions
function changeBigImage() {
    // Get elements
    let currentImageBtn = document.querySelector('.product-image-and-text-container .product-image-container .product-image-navigation-container button.active');
    let newImageBtn = this;
    let newImageButtonsrc = newImageBtn.firstElementChild.src;
    let bigImage = document.querySelector('.product-image-and-text-container .product-image-container .product-big-image');

    // Set 
    currentImageBtn.classList.remove('active');
    newImageBtn.classList.add('active');
    bigImage.src = newImageButtonsrc;
}

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

function navigateRelatedProducts() {
    let relativeProductContainer = document.querySelector('.product-related-products .product-related-products-container');

    let currentMarginLeftValue = relativeProductContainer.style.marginLeft;
    let currentMarginLeftValueRemovedPX = currentMarginLeftValue.replace('px', '');
    let currentMarginLeftValueNumber = Number(currentMarginLeftValueRemovedPX);

    let relatedProductOffset = 205;

    let leftNavButton = document.querySelector('.product-related-navigation .product-related-button-nav-left');
    let rightNavButton = document.querySelector('.product-related-navigation .product-related-button-nav-right');

    let relatedProductCount = document.querySelectorAll('.product-related-products .product-related-products-container .product-related').length;
    let relatedProductEnd = (relatedProductCount - 4) * relatedProductOffset;
    console.log(relatedProductCount);

    if (this.classList.contains('product-related-button-nav-left')) {
        relativeProductContainer.style.marginLeft = currentMarginLeftValueNumber + relatedProductOffset + 'px';
        rightNavButton.classList.remove('product-related-button-nav-hidden');

        // Hide the left nav button if it gets to the left end
        if (currentMarginLeftValueNumber + relatedProductOffset >= 0) {
            leftNavButton.classList.add('product-related-button-nav-hidden');
        }
    }

    if (this.classList.contains('product-related-button-nav-right')) {
        relativeProductContainer.style.marginLeft = currentMarginLeftValueNumber - relatedProductOffset + 'px';
        leftNavButton.classList.remove('product-related-button-nav-hidden');

        // Hide the right nav button if it gets to the right end
        if (currentMarginLeftValueNumber - relatedProductOffset <= -relatedProductEnd) {
            rightNavButton.classList.add('product-related-button-nav-hidden');
        }
    }
}

// Add event listeners
// Image navigation
for (let ImageBtn of allImageNavButtons) {
    ImageBtn.addEventListener('click', changeBigImage);
}

// Packaging selection
for (let packagingBtn of allPackagingButtons) {
    packagingBtn.addEventListener('click', changePackaging);
}

// Related products navigation
for (let relatedProductNavBtn of allRelatedProductNavButtons) {
    relatedProductNavBtn.addEventListener('click', navigateRelatedProducts);
}