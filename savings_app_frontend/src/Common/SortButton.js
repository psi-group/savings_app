const SortButton = (props) => {
  const changeSort = (e) => {
    props.setSorting(e.target.value);
  };

  return (
    <>
      <label for="sort">Sort by:</label>
      <select name="sort" onChange={changeSort}>
        <option value="by_id">Id</option>
        <option value="by_name">Name</option>
        <option value="by_price">Price</option>
      </select>
    </>
  );
};

export default SortButton;
