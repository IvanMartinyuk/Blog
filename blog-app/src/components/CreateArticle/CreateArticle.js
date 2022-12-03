import { Redirect } from 'react-router-dom/cjs/react-router-dom.min';
import { useState } from 'react/cjs/react.development';
import { userService } from '../../services/userService';
import galka from './galka.png'
import './CreateArticle.css'
import { articleService } from '../../services/articleService';



function CreateArticle(props) {
    function sets() {
    setHidden(!service.createArticle({
        image: image,
        header: header,
        content: content
    }))
    setImage('https://miro.medium.com/max/1200/1*mk1-6aYaf_Bes1E3Imhc0A.jpeg')
    setHeader('')
    setContent('')
    setTimeout(() => setHidden(true), 1000)
}
    const [image, setImage] = useState('https://miro.medium.com/max/1200/1*mk1-6aYaf_Bes1E3Imhc0A.jpeg')
    const [header, setHeader] = useState('')
    const [content, setContent] = useState('')
    const [hidden, setHidden] = useState(true)
    const service = new articleService()
    if(!userService.IsLogin)
        props.history.push('/login');
    return (
        <div className='form flex'>
            <div>
                <div className='galka'>
                    <img className='i' src={galka} hidden={hidden}></img>
                </div>
                <div className='line fl'>
                    <div>
                        <div className='nm'>
                            <label>Image url</label>
                        </div>
                        <input type='text' value={image} onChange={e => setImage(e.target.value)} className='textinput nw'></input>
                    </div>
                    <div className='imageDiv'>
                        <img className='i' src={image}></img>
                    </div>
                </div>
                <div className='line'>
                    <div>
                        <label>Header</label>
                    </div>
                    <input type='text' value={header} onChange={e => setHeader(e.target.value)} className='textinput'></input>
                </div>
                <div className='line'>
                    <div>
                        <label>Content</label>
                    </div>
                    <textarea value={content} onChange={e => setContent(e.target.value)}></textarea>
                </div>
                <div className='line'>
                    <button className='button' onClick={e => sets()}>Save</button>
                </div>
            </div>
        </div>
    )
}

export default CreateArticle;