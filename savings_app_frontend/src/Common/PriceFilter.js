import Typography from '@material-ui/core/Typography';
import Slider from '@material-ui/core/Slider';
const PriceFilter = (props) => {
   const changeSort = () => {}
  
    return (
      
        
        <div className="border-sky-500 border-1 rounded-md px-1 outline-none hover:bg-sky-100 focus:bg-sky-500 focus:text-white" onChange={changeSort}>
          <Typography id="range-slider" gutter-bottom>
            Select Price Range:
          </Typography>
        </div>
      
    );
  };
  
  export default PriceFilter;
  