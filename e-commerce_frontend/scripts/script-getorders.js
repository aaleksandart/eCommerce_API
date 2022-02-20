const form = document.getElementById('getordersform')
const submit = document.getElementById('btn-submit')
const textarea_orders = document.getElementById('getorders_textarea')
const textarea_state = document.getElementById('getordersstate')
let orderlines = []

checkIfLoggedIn()

function getOrders() {
    textarea_orders.innerHTML = ""
    fetch('https://localhost:7250/api/Orders?usercode=YXJlIHlvdSB1c2VyIG9yIG5vdA==' ,{
        method: 'get',
        headers: {
            "Content-Type": "application/json",
            "Authorization": (`bearer ${sessionStorage.getItem("accessToken")}`)
        }
    }) 
        .then(response => response.json())
        .then(data => {data.forEach(element => {
        let stringss = "";
        element.orderLines.forEach(item => {
            stringss += `          Product-ID: ${item.product.id}       Category: ${item.product.categoryName}          Product-name: ${item.product.productName}
            Price per unit: ${item.price}               Quantity: ${item.quantity}\n\n`
        });
        textarea_orders.innerHTML +=(`
        ID: ${element.id}     Name: ${element.customerName}    Email: ${element.customerEmail}     Phonenumber: ${element.customerPhoneNumber}      
        Streetname: ${element.customerStreetName}           Postalcode: ${element.customerPostalCode}     
        City: ${element.customerCity}         Country: ${element.customerCountry}         Totalprice: ${element.totalPrice}       Orderstate: ${element.orderState}    
        Created date: ${element.createdDate}      Updated date: ${element.updatedDate}
        Orderlines:  \n${stringss}
        `)
    });})
    }

    form.addEventListener('click', function(e) {
        e.preventDefault()
        getOrders()
    })

    function checkIfLoggedIn() {
        if((`${sessionStorage.getItem("accessToken")}`).length < 40) {
            textarea_state.value = "You need to log in."
        }
    }