// Header JS logic
// Variable definitions
let menuIcon = document.getElementById('menu-icon');

// Function definitions
function manageHeaderOnScroll() {
    // Variable definitions
    let headerElement = document.querySelector('header');
    let headerElementClass = headerElement.className;
    let windowScrollLocation = window.scrollY;

    // If window location is under 50px 
    // and the header sticky class isn't present - return
    if (windowScrollLocation < 50 && !headerElementClass) {
        return;
    }

    // If window location is over 50px 
    // and the header sticky class isn't present - add it
    if (windowScrollLocation > 50 && !headerElementClass) {
        headerElement.classList.add('header-sticky');
        return;
    }

    // If window location is over 50px 
    // and the header sticky class is present - return
    if (windowScrollLocation > 50 && headerElementClass) {
        return;
    }

    // If window location is under 50px 
    // and the header sticky class is present - remove it
    if (windowScrollLocation < 50 && headerElementClass) {
        headerElement.classList.remove('header-sticky');
        return;
    }
}

function toggleMobileMenu() {
    let mainMenu = document.getElementById('main-menu');

    if (mainMenu.style.display == 'flex') {
        mainMenu.style.display = 'none';
        return;
    }

    mainMenu.style.display = 'flex';
}

// Add event listeners
document.addEventListener('scroll', manageHeaderOnScroll);
menuIcon.addEventListener('click', toggleMobileMenu);
