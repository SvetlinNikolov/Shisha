// Requests to BE JS logic
// POST
async function postRequest(url = '', data = {}) {
    const response = await fetch(url, {
        method: 'POST',
        mode: 'cors',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    });

    return response.json();
}

// POST - how to use
// postRequest('addUrl', { add: 'data' })
    // .then(data => {
    //     console.log(data);
    // });

// GET
async function getRequest(url = '') {
    const response = await fetch(url);

    return response.json();
}

// GET - how to use
// getRequest('addUrl')
//     .then(data => {
//         console.log(data);
//     });