import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:44398',
})

export default api;