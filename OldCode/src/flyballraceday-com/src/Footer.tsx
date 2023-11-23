import { Copyright } from '@mui/icons-material';
import { Box, Link, Typography } from '@mui/material';
import * as React from 'react';

const Footer: React.FC = () => {

    function Copyright() {
        return (
          <Typography variant="body2" color="text.secondary" align="center">
            {'Copyright © '}
            <Link color="inherit" href="https://flyballraceday.com/">
              Flyball Raceday
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
          </Typography>
        );
      }

    return (
        <Box sx={{ bgcolor: 'background.paper', p: 6 }} component="footer">        
            <Copyright />
        </Box>    
    )
}

export default Footer;