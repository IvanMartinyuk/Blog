import { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { userService } from '../../services/userService';
import './LogIn.css'

function LogIn() {
    const [name, setName] = useState('')
    const [password, setPassword] = useState('')
    const uservice = new userService();
    return (
        <div className='flex b'>
            <div>
                <h1 className='text'>Login</h1>
                <div className='line'>
                    <div className='text'>
                        Name
                    </div>
                    <div>
                        <input type="text" value={name} onChange={e => setName(e.target.value)} className='input'></input>
                    </div>
                </div>
                <div className='line'>
                    <div className='text'>
                        Password
                    </div>
                    <div>
                        <input type="password" value={password} onChange={e => setPassword(e.target.value)} className='input'></input>
                    </div>
                </div>
                <div className='line'>
                    <div className='flex'>
                        <NavLink onClick={() => uservice.Login(name, password)} className='button' to={"../main"}>Login</NavLink>
                        <button onClick={() => uservice.Registration(name, password)} className='button'>Register</button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default LogIn;