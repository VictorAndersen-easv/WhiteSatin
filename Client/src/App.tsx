import { APITester } from "./APITester";
import "./index.css";

import logo from "./logo.svg";
import reactLogo from "./react.svg";
import {Api, type BookDto} from "../Api.ts";
import {useEffect, useState} from "react";
import toast from "react-hot-toast";

const MyApi = new Api();

export function App() {

   return <div>
       Hello World!
       <a target={"_blank"} href={"https://www.w3schools.com/css/paris.jpg"}>
           <img src={"https://www.w3schools.com/css/paris.jpg"} alt="paris" />
       </a>

   </div>
}

export default App;
