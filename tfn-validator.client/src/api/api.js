export const fetchData = async (inputValue) => { 
    try
    {
        const params = new URLSearchParams({
            TFN: inputValue,
        });

        const response = await fetch(`https://localhost:7089/api/TFNValidator?${params}`);
        const result = await response.text();
        if (!response.ok) throw new Error(result);

        return result;
    }
    catch (error)
    {
        console.error("Fetch error:", error);
        throw error;
    }    
};
