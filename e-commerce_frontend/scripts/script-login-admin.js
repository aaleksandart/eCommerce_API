const loginform = document.getElementById('loginadmin-form')
const login = document.getElementById('btn-loginadmin')
const loginemail = document.getElementById('loginemailadmin_input')
const loginpassword = document.getElementById('loginpasswordadmin_input')
const loginstate = document.getElementById('loginstateadmin')

checkIfLoggedIn()

function logIn(loginData) {
    fetch('https://localhost:7250/api/Authentication/SignInAdmin', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: loginData
    })
    .then(res => res.text())
    .then(data => {
        sessionStorage.setItem("accessToken-admin", data),
        sessionStorage.setItem("accessToken", data)
    })
}

loginform.addEventListener('submit', function(e) {
    e.preventDefault()
    let loginInfo = JSON.stringify({
        email: loginemail.value, 
        password: loginpassword.value
    })
    logIn(loginInfo)
    loginemail.value = ""
    loginpassword.value = ""
    window.location.reload();
})

function checkIfLoggedIn() {
    if((`${sessionStorage.getItem("accessToken-admin")}`).length > 50) {
        loginstate.value = "You are logged in as a admin."
    }
    else if((`${sessionStorage.getItem("accessToken")}`).length > 50) {
        loginstate.value = "You are logged in as a user."
    }
}