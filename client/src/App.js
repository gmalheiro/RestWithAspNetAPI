import { useState } from "react";
import Header from "./Header";

export default function App() {
  const [counter,setCounter] = useState(0);

  const increment = () => {
    setCounter(counter + 1)
  }

  return (
    <div class="">
      <Header>
        {counter}
      </Header>
      <button  onClick={increment} >Click me</button>
    </div>
  );
}