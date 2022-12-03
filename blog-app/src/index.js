import React from 'react';
import ReactDOM from 'react-dom';
import { Route, NavLink, BrowserRouter as Router, Switch } from 'react-router-dom'
import Article from './components/Article/Article';
import CreateArticle from './components/CreateArticle/CreateArticle';
import LogIn from './components/LogIn/LogIn';
import Main from './components/Main/Main';
import News from './components/News/News';
import './index.css';

ReactDOM.render(
  <Router>
    <div className='navbar'>
      <ul>
        <il><NavLink exact activeClassName="active" to="/"></NavLink></il>
        <il><NavLink activeClassName="active" to="/main" className='link'>Main</NavLink></il>
        <il><NavLink activeClassName="active" to="/news" className='link'>News</NavLink></il>
        <il><NavLink activeClassName="active" to="/createarticle" className='link'>New article</NavLink></il>
        <il><NavLink activeClassName="active" to="/login" className='link'>Log In</NavLink></il>
      </ul>
    </div>
    <div className='content'>
      <div className='h'>
        <Switch>
          <Route exact path='/' component={Main}></Route>
          <Route exact path='/main' component={Main}></Route>
          <Route exact path='/news' component={News}></Route>
          <Route exact path='/createarticle' component={CreateArticle}></Route>
          <Route exact path='/login' component={LogIn}></Route>
          <Route exact path='/article/:articleId' component={Article}></Route>
        </Switch>
      </div>
    </div>
  </Router>,
  document.getElementById('root')
);