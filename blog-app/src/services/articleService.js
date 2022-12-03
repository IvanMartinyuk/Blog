export class articleService {
    async createArticle(article) {
        const token = sessionStorage.getItem('access_token')
        let response = await fetch('https://localhost:44343/article/post', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + token
            },
            body: JSON.stringify(article)
        })
        return response.ok;
    }
    async getArticles() {
        let response = await fetch('https://localhost:44343/article/getall', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        let data = await response.json()
        return data
    }
    async getArticle(id) {
        let response = await fetch('https://localhost:44343/article/get?id=' + id, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        let data = await response.json()
        return data
    }
}