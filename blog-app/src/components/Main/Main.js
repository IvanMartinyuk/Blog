import { useEffect } from 'react';
import { NavLink } from 'react-router-dom';
import { useState } from 'react/cjs/react.development';
import { articleService } from '../../services/articleService';
import './Main.css'

function Main(props) {
    const [articles, setArticles] = useState([])
    const service = new articleService()
    useEffect(() => {
        service.getArticles().then(data => setArticles(data))
    }, [])
    let result = []

    articles.forEach(article => {
        article.dateOfPublish = (new Date(article.dateOfPublish))
        result.push(
        <div className='box'>
            <div className='card'>
                <div className='textPart'>
                    <h1 className='textHeader' onClick={() => props.history.push('../article/' + article.articleId)}>{article.header}</h1>
                    <div className='textContent'>{article.content.substr(0, 500)}</div>
                </div>
                <div className='imageD'>
                    <img src={article.image} className='ii'></img>
                </div>
            </div>
            <div className='date'>
                {article.dateOfPublish.getDay() + '.' + article.dateOfPublish.getMonth() + '.' + article.dateOfPublish.getFullYear()}
            </div>
        </div>)
    });
    return (
        <div>
            <div className='header'>
                <h1 className='bigText'>MyBlog is a place to write, read, and connect</h1>
                <NavLink className='blackBtn' to={"../createarticle"}>Get started</NavLink>
            </div>
            <div>
                {result}
            </div>
        </div>
    )
}

export default Main;