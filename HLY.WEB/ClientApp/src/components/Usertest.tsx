import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as UsersStore from '../store/Users';

type UsersProps =
UsersStore.UsersState &
    typeof UsersStore.actionCreators &
    RouteComponentProps<{}>;

class Userstest extends React.PureComponent<UsersProps> {
    public render() {
        return (
            <React.Fragment>
                <form name="form">
                    <div className={'form-group'}>
                        <label htmlFor="username">Username</label>
                        <input type="text" className="form-control" name="username" value={"this.state.code"} 
                        onChange={e => this.setState({code:e.target.value})} />
                    </div>
                    <div className={'form-group'}>
                        <label htmlFor="password">Password</label>
                        <input type="password" className="form-control" name="password" value={"this.state.password"} 
                        onChange={e => this.setState({password:e.target.value})} />
                    </div>
                    <div className="form-group">
                        <button className="btn btn-primary" onClick={() => { this.props.requestUsers("this.state.code","")}}>Login</button>
                    </div>
                </form>

                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => { this.props.requestUsers("this.state.code",""); }}>
                    Increment
                </button>
            </React.Fragment>
        );
    }
};

export default connect(
    (state: ApplicationState) => state.Users,
    UsersStore.actionCreators
)(Userstest as any);;
