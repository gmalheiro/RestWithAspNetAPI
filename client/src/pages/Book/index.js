import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { FiPower, FiEdit, FiTrash2 } from 'react-icons/fi'

import './styles.css';

import logoImage from '../../assets/logo.svg'

export default function Login (){
    return (
        <div className="book-container">
            <header>
                <img src={logoImage} alt="Erudio"/>
                <span>Welcome, <strong>Gabriel</strong>!</span>
                <Link className="button" to="book/new/0">Add New Book</Link>
                <button  type="button">
                    <FiPower size={18} color="#251FC5" />
                </button>
            </header>
        </div>
    );
}