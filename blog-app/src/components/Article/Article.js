import { useEffect } from 'react';
import { useState } from 'react/cjs/react.development'
import { articleService } from '../../services/articleService';
import './Article.css'

function Article(props) {
    const [article, setArticle] = useState(
        {
            image: '',
            header: '',
            content: ''
        }
    )
    const service = new articleService()
    useEffect(() => {
        service.getArticle(props.match.params.articleId).then(data => {
            setArticle(data)
        })
    }, []) 
    return(
        <div>
            <div className='imageDi'>
                <img src={article.image}></img>
            </div>
            <h1>{article.header}</h1>
            <div>
                {article.content}
            </div>
        </div>
    )
}

export default Article