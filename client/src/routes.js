import React from 'react';
import {BrowserRouter, Route,Switch} from 'react-router-dom';

import Login from './pages/Login';
import Book from './pages/Book';
import NewBook from './pages/NewBook';


export default function Routes() {
    return (
        <BrowserRouter>
            <Switch>
                <Route path="/" exact component={Login}/>
                <Route path="/login" exact component={Login}/>
                <Route path="/books" exact component={Book}/>
                <Route path="/book/new" exact component={NewBook}/>
            </Switch>
        </BrowserRouter>
    );
}