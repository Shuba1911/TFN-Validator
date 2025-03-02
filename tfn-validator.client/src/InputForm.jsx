import { useState } from "react";
import { fetchData } from "./api/api"

const InputForm = () => {
    const [inputValue, setInputValue] = useState("");
    const [responseData, setResponseData] = useState(null);
    const [error, setError] = useState(null);


    const handleSubmit = async (e) => {
        e.preventDefault(); 
        try {
            if (!inputValue.trim()) {
                setError("TFN is required");
            } else {
                const data = await fetchData(inputValue);
                setError(null);
                setResponseData(data);
            }
            
        } catch (error) {
            setResponseData(null);
            setError(error.message);
        } 
    };

    return (
        <form onSubmit={handleSubmit}>
            <h3>TFN Validator</h3>
            <input
                type="text"
                value={inputValue}
                onChange={(e) => setInputValue(e.target.value)}
                placeholder="Enter TFN..."
            />
            <button type="submit">
                Submit
            </button>
            <div>
                <p className="text-red">{error}</p>
                <p>{responseData}</p>
            </div>
        </form>
        
       
    );
};

export default InputForm;
