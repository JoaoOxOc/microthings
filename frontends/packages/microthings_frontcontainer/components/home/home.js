import React, { useState } from "react";

import { Header } from "../header/header";

export function Home({ history }) {
    const [input, setInput] = useState("");
  
    const handleOnClick = () => {
      history.push(`/otherapp/${input}`);
    };
  
    return (
      <div>
        <Header />
        <div className="home">
          <input
            placeholder="Insert a greeting"
            value={input}
            onBlur={(e) => setInput(e.target.value)}
          />
          <button onClick={handleOnClick}>Greet Me</button>
        </div>
  
        <div className="home">
          <div className="content">
          </div>
        </div>
      </div>
    );
  }