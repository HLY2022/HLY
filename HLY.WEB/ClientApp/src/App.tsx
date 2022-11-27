import * as React from 'react';
//import { BrowserRouter, Route } from 'react-router';
import {BrowserRouter,Route,Link} from 'react-router-dom'
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Login from './pages/login/Login';
import Usertest from './components/Usertest';

import './custom.css'

export default () => (
    <BrowserRouter>
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
            <Route path='/login' component={Login} />
            <Route path='/usertest' component={Usertest} />
        </Layout>
    </BrowserRouter>
);
