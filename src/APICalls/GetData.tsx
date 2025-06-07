import { useEffect, useState } from "react"; 
import type { APIResponse } from "../app/Models/APIResponse";
import axios from "axios";


 

export default function GetData<T>(url: string) {
  const [response, setResponse] = useState<APIResponse<T>>();

  const getData = async () => {
    const { data }: { data: APIResponse<T> } = await axios.get(url);
    setResponse(data);
    console.log(`GetData => ${url}`)
  }

  useEffect(() => {
    getData()
    
  }, []);

  return {
    response
  }
}