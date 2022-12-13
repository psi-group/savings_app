const SortButton = (props) => {
  const changeSort = (e) => {
    props.setSorting(e.target.value);
  };

  return (
    <>

<select name="sort" className="border-sky-500 border-1 p-1 rounded-md px-4 outline-none hover:bg-sky-100 focus:bg-sky-500 focus:text-white" onChange={changeSort}>
        <option className="group-focus:bg-sky-600" value="by_id">Sort By</option>
        <option value="by_id">Id</option>
        <option value="by_name">Name</option>
        <option value="by_price">Price</option>
      </select>
    </>
  );
};

export default SortButton;
