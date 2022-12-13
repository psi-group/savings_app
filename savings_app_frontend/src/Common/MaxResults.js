const MaxResults = () => {
    
  
    return (
      <>
  
  <select name="maxResults" className="border-sky-500 border-1 p-1 rounded-md px-4 outline-none hover:bg-sky-100 focus:bg-sky-500 focus:text-white">
          <option className="group-focus:bg-sky-600" value="maxResults">Limit results: </option>
          <option value="24">24</option>
          <option value="36">36</option>
          <option value="48">48</option>
        </select>
      </>
    );
  };
  
  export default MaxResults;
  