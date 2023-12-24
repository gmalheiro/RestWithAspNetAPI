import React, { useState, useEffect } from "react";
import { Link, useHistory } from "react-router-dom";
import { FiPower, FiEdit, FiTrash2,FiArrowLeft } from "react-icons/fi";

import "./styles.css";

import logoImage from "../../assets/logo.svg";

export default function Books() {
  return (
    <div className="new-book-container">
      <div className="content">
        <section className="form">
          <img src={logoImage} alt="Erudio" />
          <h1>Add new Book</h1>
          <p>Enter the book information and click on 'Add' !</p>
          <Link className="back-link" to="/books">
            <FiArrowLeft size={16} color="#251fc5" />
            Back to Books
          </Link>
        </section>

        <form>
          <input placeholder="Title" />
          <input placeholder="Author" />
          <input type="date" />
          <input placeholder="Price" />

          <button className="button" type="submit">
            Add
          </button>
        </form>
      </div>
    </div>
  );
}
