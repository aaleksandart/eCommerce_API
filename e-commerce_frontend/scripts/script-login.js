const loginform = document.getElementById('login-form')
const login = document.getElementById('btn-login')
const loginemail = document.getElementById('loginemail_input')
const loginpassword = document.getElementById('loginpassword_input')
const loginstate = document.getElementById('loginstate')
loginstate.value = ""

checkIfLoggedIn()

function logIn(loginData) {
    fetch('https://localhost:7250/api/Authentication/SignIn', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: loginData
    })
    .then(res => res.text())
    .then(data => {
        sessionStorage.setItem("accessToken-admin", ""),
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
    window.location.reload()
})

function checkIfLoggedIn() {
    if((`${sessionStorage.getItem("accessToken")}`).length > 50 && (`${sessionStorage.getItem("accessToken-admin")}`).length > 50) {
        loginstate.value = "You are logged in as a admin."
    }
    else if((`${sessionStorage.getItem("accessToken")}`).length > 50) {
        loginstate.value = "You are logged in as a user."
    }
}