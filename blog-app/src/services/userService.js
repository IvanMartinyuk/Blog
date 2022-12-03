export class userService {
    static IsLogin = false
    async Login(name, password) {
        let user = {
            username: name,
            password: password
        }
        let response = await fetch('https://localhost:44343/user/token', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        if(response.ok === true)
            userService.IsLogin = true
        let data = await response.json()
        sessionStorage.setItem('access_token', data.access_token)
    }
    async Registration(name, password) {
        let user = {
            username: name,
            password: password
        }

        let response = await fetch('https://localhost:44343/user/registration', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        let data = await response.json()
        return data
    }

}